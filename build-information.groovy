import groovy.json.JsonBuilder;
import java.util.Date;
   
class BuildInformation {
    long buildDate;
    String gitRevision;
    String gitShortHash;
    String gitLongHash;
    String gitBranch;
    int buildNumber;
}

def runGitCommand(String command) {
  command =   "git -C " + build.workspace.toString() + "/ " + command;
  println "Running command: " + command;
  def proc = command.execute();
  return proc.text.trim();
}

def env = System.getenv();

def buildInfo = new BuildInformation()
// Define the properies
buildInfo.buildDate = new Date().getTime();
buildInfo.gitRevision = runGitCommand("rev-list --count HEAD");
buildInfo.gitShortHash = runGitCommand("rev-parse --short HEAD");
buildInfo.gitLongHash = runGitCommand("rev-parse HEAD");
buildInfo.gitBranch ="master";
buildInfo.buildNumber = build.number;

// Create the JSON builder
def builder = new JsonBuilder();

builder buildInformation: buildInfo;
json = builder.toPrettyString()

println "Build Information: " + json;

// Write the json to file
def configurationFile = build.workspace.toString() + "/buildInformation.json";
new File(configurationFile ).write(json);
println "Wrote configuration to: " + configurationFile;