using Godot;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;

[Tool]
public partial class BuildOnChangeOnly : EditorPlugin
{
    FileSystemWatcher fileSystemWatcher;
    volatile bool hasChanged = false;
    Node monoPlugin;
    public void Start() {
        // Find the built-in C# support plugin.
        monoPlugin = GetParent().GetParent().GetChildren().FirstOrDefault(n => n.HasMethod("BuildProjectPressed"));
        monoPlugin.Set("SkipBuildBeforePlaying", true);
        fileSystemWatcher = new FileSystemWatcher(ProjectSettings.GlobalizePath("res://")) {
            Filter = "*.cs",
            IncludeSubdirectories = true
        };
        fileSystemWatcher.Changed += FileChanged;
        fileSystemWatcher.Created += FileChanged;
        fileSystemWatcher.Deleted += FileChanged;
        fileSystemWatcher.Error += (s, e) => GD.Print(e.GetException().Message);
        fileSystemWatcher.EnableRaisingEvents = true;

        EditorInterface.Singleton.GetBaseControl().GetWindow().FocusEntered += Focus;
        GD.Print("(Re)starting the BuildOnChangeOnly plugin.");
    }
    public void End() {
        EditorInterface.Singleton.GetBaseControl().GetWindow().FocusEntered -= Focus;

        fileSystemWatcher.EnableRaisingEvents = false;
        fileSystemWatcher.Changed -= FileChanged;
        fileSystemWatcher.Created -= FileChanged;
        fileSystemWatcher.Deleted -= FileChanged;
        fileSystemWatcher.Dispose();

        monoPlugin.Set("SkipBuildBeforePlaying", false);
        GD.Print("Stopping the BuildOnChangeOnly plugin.");
    }
    private void FileChanged(object Sender, FileSystemEventArgs e) {
        if (!hasChanged) {
            GD.Print("C# file changed, rebuild queued.");
            hasChanged = true;
        }
    }
    private void Focus() {
        if (hasChanged) {
            GetParent().Call("Build");
        }
    }
}