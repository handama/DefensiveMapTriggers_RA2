using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rampastring.Tools;

namespace DefensiveMapTriggers_RA2
{
    public class FullTrigger
    {
        public Action Action;
        public Event Event;
        public Tag Tag;
        public Trigger Trigger;
        public FullTrigger(Action Action,Event Event, Trigger Trigger, int repeat)
        {
            this.Action = Action;
            this.Event = Event;
            this.Trigger = Trigger;
            Tag = new Tag(Trigger.index + 1, Trigger.name, Trigger.index, repeat);
            Program.GetIndex();
        }
        public void WriteInIni()
        {
            var actions = Program.Output.GetSection("Actions");
            var events = Program.Output.GetSection("Events");
            var tags = Program.Output.GetSection("Tags");
            var triggers = Program.Output.GetSection("Triggers");

            actions.AddKey(Action.GetKey(), Action.GetValue());
            events.AddKey(Event.GetKey(), Event.GetValue());
            tags.AddKey(Tag.GetKey(), Tag.GetValue());
            triggers.AddKey(Trigger.GetKey(), Trigger.GetValue());
        }
    }
    public class Action
    {
        public int index;
        public int number;
        public List<string> value;
        public Action(int index, List<string> value)
        {
            this.index = index;
            number = value.Count;
            this.value = value;
        }
        public string GetKey()
        {
            return Program.GetIndexString(index);
        }
        public string GetValue()
        {
            //3,53,2,0100009,0,0,0,0,A,53,2,0100017,0,0,0,0,A,54,2,0100001,0,0,0,0,A
            string value = "";
            value += number.ToString() + ",";
            foreach(var v in this.value)
            {
                value += v + ",";
            }
            return value.TrimEnd(',');
        }
    }
    public class Event
    {
        public int index;
        public int number;
        public List<string> value;
        public Event(int index,List<string> value)
        {
            this.index = index;
            number = value.Count;
            this.value = value;
        }
        public string GetKey()
        {
            return Program.GetIndexString(index);
        }
        public string GetValue()
        {
            //1,1,0,4475
            string value = "";
            value += number.ToString() + ",";
            foreach (var v in this.value)
            {
                value += v + ",";
            }
            return value.TrimEnd(',');
        }
    }
    public class Tag
    {
        public int index;
        public int repeat;
        public string name;
        public int triggerIndex;
        public Tag(int index, string name, int triggerIndex, int repeat = 0)
        {
            this.index = index;
            this.repeat = repeat;
            this.name = name;
            this.triggerIndex = triggerIndex;
        }
        public string GetKey()
        {
            return Program.GetIndexString(index);
        }
        public string GetValue()
        {
            //2,city 1 captured by A 1,0100001
            string value = "";
            value += repeat.ToString() + ",";
            value += name;
            value += " 1,";
            value += Program.GetIndexString(triggerIndex);
            return value;
        }
    }
    public class Trigger
    {
        public int index;
        public string house;
        public string attachedTrigger;
        public string name;
        public bool disabled;
        public bool enableEasy;
        public bool enableMedium;
        public bool enableHard;
        public Trigger(int index, string name, string house = "Americans", string attachedTrigger = "<none>", bool disabled = false, bool enableEasy = true, bool enableMedium = true, bool enableHard = true)
        {
            this.index = index;
            this.name = name;
            this.house = house;
            this.attachedTrigger = attachedTrigger;
            this.disabled = disabled;
            this.enableEasy = enableEasy;
            this.enableMedium = enableMedium;
            this.enableHard = enableHard;
        }
        public string GetKey()
        {
            return Program.GetIndexString(index);
        }
        string getBoolString(bool b)
        {
            string result = "0";
            if (b)
                result = "1";
            return result;
        }

        public string GetValue()
        {
            //Americans,0100002,city 1 captured by A,0,1,1,1,0
            string value = "";
            value += house + ",";
            value += attachedTrigger + ",";
            value += name + ",";
            value += getBoolString(disabled) + ",";
            value += getBoolString(enableEasy) + ",";
            value += getBoolString(enableMedium) + ",";
            value += getBoolString(enableHard) + ",";
            value += "0";
            return value;
        }
    }
    public class Team
    {
        public int index;

        public int Max = 5;
        public bool Full = false;
        public string Name = "New teamtype";
        public int Group = -1;
        public string House ="<Player @ A>";
        public Script Script;
        public bool Whiner = false;
        public bool Droppod = false;
        public bool Suicide = false;
        public bool Loadable = false;
        public bool Prebuild = false;
        public int Priority = 5;
        public string Waypoint = "A";
        public bool Annoyance = false;
        public bool IonImmune = false;
        public bool Recruiter = false;
        public bool Reinforce = false;
        public TaskForce TaskForce;
        public int TechLevel = 0;
        public bool Aggressive = true;
        public bool Autocreate = false;
        public bool GuardSlower = false;
        public bool OnTransOnly = false;
        public bool AvoidThreats = false;
        public bool LooseRecruit = false;
        public int VeteranLevel = 0;
        public bool IsBaseDefense = false;
        public bool UseTransportOrigin = false;
        public int MindControlDecision = 0;
        public bool OnlyTargetHouseEnemy = false;
        public bool TransportsReturnOnUnload = false;
        public bool AreTeamMembersRecruitable = false;

        public Team(int index)
        {
            this.index = index;
        }
        public void WriteInIni()
        {
            string index = Program.GetIndexString(this.index);
            var teamIndex = Program.Output.GetSection("TeamTypes");
            teamIndex.AddKey(Program.GetIniIndex("TeamTypes"), index);

            Program.Output.AddSection(index);
            var team = Program.Output.GetSection(index);
            team.AddKey("Max", Max.ToString());
            team.AddKey("Full", Full.YesOrNo());
            team.AddKey("Name", Name);
            team.AddKey("Group", Group.ToString());
            team.AddKey("House", House);
            if (Script != null)
                team.AddKey("Script", Script.indexS);
            else
                team.AddKey("Script", "");
            team.AddKey("Whiner", Whiner.YesOrNo());
            team.AddKey("Droppod", Droppod.YesOrNo());
            team.AddKey("Suicide", Suicide.YesOrNo());
            team.AddKey("Loadable", Loadable.YesOrNo());
            team.AddKey("Prebuild", Prebuild.YesOrNo());
            team.AddKey("Priority", Priority.ToString());
            team.AddKey("Waypoint", Waypoint);
            team.AddKey("Annoyance", Annoyance.YesOrNo());
            team.AddKey("IonImmune", IonImmune.YesOrNo());
            team.AddKey("Recruiter", Recruiter.YesOrNo());
            team.AddKey("Reinforce", Reinforce.YesOrNo());
            if (TaskForce != null)
                team.AddKey("TaskForce", TaskForce.indexS);
            else
                team.AddKey("TaskForce", "");

            team.AddKey("TechLevel", TechLevel.ToString());
            team.AddKey("Aggressive", Aggressive.YesOrNo());
            team.AddKey("Autocreate", Autocreate.YesOrNo());
            team.AddKey("GuardSlower", GuardSlower.YesOrNo());
            team.AddKey("OnTransOnly", OnTransOnly.YesOrNo());
            team.AddKey("AvoidThreats", AvoidThreats.YesOrNo());
            team.AddKey("LooseRecruit", LooseRecruit.YesOrNo());
            team.AddKey("VeteranLevel", VeteranLevel.ToString());
            team.AddKey("IsBaseDefense", IsBaseDefense.YesOrNo());
            team.AddKey("UseTransportOrigin", UseTransportOrigin.YesOrNo());
            team.AddKey("MindControlDecision", MindControlDecision.ToString());
            team.AddKey("TransportsReturnOnUnload", TransportsReturnOnUnload.YesOrNo());
            team.AddKey("AreTeamMembersRecruitable", AreTeamMembersRecruitable.YesOrNo());
        }
    }
    public class TaskForce
    {
        public int index;
        public string indexS;

        public string name = "New TaskForce";
        List<string> value;
        int group = -1;
        public TaskForce(int index, List<string> value, int group = -1)
        {
            this.index = index;
            indexS = Program.GetIndexString(this.index);
            this.value = value;
            this.group = group;
        }
        public void WriteInIni()
        {
            string index = Program.GetIndexString(this.index);
            var taskIndex = Program.Output.GetSection("TaskForces");
            taskIndex.AddKey(Program.GetIniIndex("TaskForces"), index);

            Program.Output.AddSection(index);
            var task = Program.Output.GetSection(index);
            

            task.AddKey("Name", name);
            foreach (var v in value)
            {
                task.AddKey(Program.GetIniIndex(index), v);
            }
            task.AddKey("Group", group.ToString());
        }
    }
    
    public class Script
    {
        public int index;
        public string indexS;

        public string name = "New Script";
        List<string> value;
        public Script(int index, List<string> value)
        {
            this.index = index;
            indexS = Program.GetIndexString(this.index);
            this.value = value;
        }
        public void WriteInIni()
        {
            string index = Program.GetIndexString(this.index);
            var scriptIndex = Program.Output.GetSection("ScriptTypes");
            scriptIndex.AddKey(Program.GetIniIndex("ScriptTypes"), index);

            Program.Output.AddSection(index);
            var script = Program.Output.GetSection(index);


            script.AddKey("Name", name);
            foreach (var v in value)
            {
                script.AddKey(Program.GetIniIndex(index), v);
            }
        }
    }

    public class Attack
    {
        string name;
        int StartTime;
        int CycleTime;
        int RepeatTimes;
        List<string> TaskForce = new List<string>();
        List<int> StartWaypoint = new List<int>();
        int VeteranLevel;
        bool AvoidThreats;
        bool Suicide;
        bool Droppod;
        bool Chronosphere;
        bool Full;
        bool AreTeamMembersRecruitable;
        bool RecruitFromExistingUnits;
        List<string> Script = new List<string>();

        public Attack(string name)
        {
            this.name = name;
            var section = Program.Settings.GetSection(name);
            StartTime = section.GetIntValue("StartTime", 120);
            CycleTime = section.GetIntValue("CycleTime", 60);
            RepeatTimes = section.GetIntValue("RepeatTimes", 10);
            int teamindex = 0;
            while (section.KeyExists("TaskForce" + teamindex.ToString()))
            {
                TaskForce.Add(section.GetStringValue("TaskForce" + teamindex.ToString(), "5,HTNK"));
                Script.Add(section.GetStringValue("Script" + teamindex.ToString(), "0,0"));
                StartWaypoint.Add(section.GetIntValue("StartWaypoint" + teamindex.ToString(), 0));
                teamindex++;
            }
            
            
            VeteranLevel = section.GetIntValue("VeteranLevel", 0);
            AvoidThreats = section.GetBooleanValue("AvoidThreats", false);
            Suicide = section.GetBooleanValue("Suicide", false);
            Droppod = section.GetBooleanValue("Droppod", false);
            Chronosphere = section.GetBooleanValue("Chronosphere", false);
            Full = section.GetBooleanValue("Full", false);
            AreTeamMembersRecruitable = section.GetBooleanValue("AreTeamMembersRecruitable", false);
            RecruitFromExistingUnits = section.GetBooleanValue("RecruitFromExistingUnits", false);
            
        }
        public void WriteInIni()
        {
            int attackI = Program.GetIndex();
            var attackActVal = new List<string>();
            var action = new Action(attackI, attackActVal);
            var attackEveVal = new List<string> { $"13,0,{CycleTime}" };
            var eventt = new Event(attackI, attackEveVal);
            var trigger = new Trigger(attackI, name, disabled: true);
            var full = new FullTrigger(action, eventt, trigger, 2);   

            for (int i = 0; i < TaskForce.Count; i++)
            {
                var taskForceList = new List<string>(TaskForce[i].Split('/'));
                var task = new TaskForce(Program.GetIndex(), taskForceList);
                task.name = name + " taskforce " + i.ToString();
                task.WriteInIni();

                var scriptList = new List<string>(Script[i].Split('/'));
                var script = new Script(Program.GetIndex(), scriptList);
                script.name = name + " script " + i.ToString();
                script.WriteInIni();

                var team = new Team(Program.GetIndex());
                team.TaskForce = task;
                team.Script = script;
                team.Waypoint = Program.IntToWayPoint(StartWaypoint[i]);
                team.VeteranLevel = VeteranLevel;
                team.AvoidThreats = AvoidThreats;
                team.Suicide = Suicide;
                team.Droppod = Droppod;
                team.Full = Full;
                team.House = Program.AttackHouse;
                team.AreTeamMembersRecruitable = AreTeamMembersRecruitable;
                team.Recruiter = RecruitFromExistingUnits;
                team.Name = name + " team " + i.ToString();
                team.WriteInIni();

                if (Chronosphere)
                    full.Action.value.Add($"107,1,{Program.GetIndexString(team.index)},0,0,0,0,{Program.IntToWayPoint(StartWaypoint[i])}");
                else if (RecruitFromExistingUnits)
                    full.Action.value.Add($"4,1,{Program.GetIndexString(team.index)},0,0,0,0,");
                else
                    full.Action.value.Add($"7,1,{Program.GetIndexString(team.index)},0,0,0,0,A"); 
            }
            full.Action.number = full.Action.value.Count;
            full.WriteInIni();

            int enableI = Program.GetIndex();
            var enableActVal = new List<string> { $"53,2,{Program.GetIndexString(attackI)},0,0,0,0,A" };
            var actionE = new Action(enableI, enableActVal);
            var enableEveVal = new List<string> { $"13,0,{StartTime}" };
            var eventE = new Event(enableI, enableEveVal);
            var triggerE = new Trigger(enableI, "enable " + name);
            var fullE = new FullTrigger(actionE, eventE, triggerE, 0);
            fullE.WriteInIni();

            if (RepeatTimes>0)
            {
                int disableI = Program.GetIndex();
                var disableActVal = new List<string> { $"54,2,{Program.GetIndexString(attackI)},0,0,0,0,A" };
                var actionD = new Action(disableI, disableActVal);
                var disableEveVal = new List<string> { $"13,0,{StartTime + RepeatTimes * CycleTime + 1}" };
                var eventD = new Event(disableI, disableEveVal);
                var triggerD = new Trigger(disableI, "disable " + name);
                var fullD = new FullTrigger(actionD, eventD, triggerD, 0);
                fullD.WriteInIni();
            }
        }
    }
    static class Program
    {
        public static int Index = 0;
        public static string AttackHouse = "";
        public static IniFile Output;
        public static IniFile Settings;

        #region general function
        public static string HouseNameToIndex()
        {
            var index = "";
            if (AttackHouse == "<Player @ A>")
                index = "4475";
            else if (AttackHouse == "<Player @ B>")
                index = "4476";
            else if (AttackHouse == "<Player @ C>")
                index = "4477";
            else if (AttackHouse == "<Player @ D>")
                index = "4478";
            else if (AttackHouse == "<Player @ E>")
                index = "4479";
            else if (AttackHouse == "<Player @ F>")
                index = "4480";
            else if (AttackHouse == "<Player @ G>")
                index = "4481";
            else if (AttackHouse == "<Player @ H>")
                index = "4482";
            return index;
        }
        public static string YesOrNo(this System.Boolean target)
        {
            string result = "no";
            if (target)
                result = "yes";
            return result;
        }
        public static string GetIndexString(int i)
        {
            string index = string.Format("{0:D7}", i);
            return index;
        }
        public static string GetIniIndex(string name)
        {
            string index = "";
            var section = Output.GetSection(name);
            bool end = false;
            int i = 0;
            while (!end)
            {
                if (section.KeyExists(i.ToString()))
                {
                    i++;
                }
                else
                {
                    end = true;
                    index = i.ToString();
                }
            }
            return index;
        }
        public static int GetIndex()
        {
            Index++;
            return Index - 1;
        }
        public static string IntToWayPoint(int i)
        {
            i++;
            int first = i / 26;
            int second = i % 26;

            string fs = "";
            string ss = "";

            if (first != 0)
                fs = Cha(Asc("A") - 1 + first).ToString();
            ss = Cha(Asc("A") - 1 + second).ToString();
            return fs + ss;
        }
        private static int Asc(string s)
         {
             if (s.Length == 1)
             {
                 ASCIIEncoding a = new ASCIIEncoding();
                 int b = (int)a.GetBytes(s)[0];  //利用ASCIIEncoding类的GetByte()方法转码
                 return b;
             }
             else
             {
                 throw new Exception("String is not vaild");
             }
         }

         /*ascii码转字符*/
         private static char Cha(int a)
         {
             if (a >= 0 && a <= 255)     //若不在这个范围内，则不是字符
             {
                  char c = (char)a;   //利用类型强转得到字符
                 return c;
             }
             else
             {
                 throw new Exception("String is not vaild");
             }
         }
        #endregion
        static void Main(string[] args)
        {
            Settings = new IniFile("settings.ini");
            var general = Settings.GetSection("General");

            Output = new IniFile();

            Output.AddSection("Actions");
            Output.AddSection("Events");
            Output.AddSection("Tags");
            Output.AddSection("Triggers");
            Output.AddSection("TeamTypes");
            Output.AddSection("TaskForces");
            Output.AddSection("ScriptTypes");

            Index = general.GetIntValue("InitialSequenceNumber", 0);
            AttackHouse = general.GetStringValue("AttackHouse", "<Player @ A>");
            var winTime = general.GetIntValue("WinTime", 3600);
            var startTime = general.GetIntValue("StartTime", 120);
            string houseindex = HouseNameToIndex();

            int initialI = GetIndex();
            var initialA = new Action(initialI, new List<string> { $"14,0,{houseindex},0,0,0,0,A", $"121,0,{houseindex},0,0,0,0,A" });
            var initialE = new Event(initialI, new List<string> { "13,0,0" });
            var initialT = new Trigger(initialI, "destroy invader MCV and give buildings");
            var initialF = new FullTrigger(initialA, initialE, initialT, 0);
            initialF.WriteInIni();

            int startI = GetIndex();
            var startA = new Action(startI, new List<string> { $"27,0,{startTime},0,0,0,0,A", "103,4,Name:InvasionStart,0,0,0,0,A", "23,0,0,0,0,0,0,A" });
            var startE = new Event(startI, new List<string> { "13,0,0" });
            var startT = new Trigger(startI, "start invasion timer");
            var startF = new FullTrigger(startA, startE, startT, 0);
            startF.WriteInIni();

            int endI = GetIndex();
            var endA = new Action(endI, new List<string> { "24,0,0,0,0,0,0,A", $"27,0,{winTime},0,0,0,0,A", "103,4,Name:InvasionEnd,0,0,0,0,A", "23,0,0,0,0,0,0,A" , "19,7,NukeSiren,0,0,0,0,A", "11,4,Name:InvasionStarted,0,0,0,0,A" });
            var endE = new Event(endI, new List<string> { $"13,0,{startTime}" });
            var endT = new Trigger(endI, "end invasion timer");
            var endF = new FullTrigger(endA, endE, endT, 0);
            endF.WriteInIni();

            var sections = Settings.GetSections();
            foreach (var sec in sections)
            {
                if (sec != "General")
                {
                    var attack = new Attack(sec);
                    attack.WriteInIni();
                }
            }

            if (winTime > 0)
            { 
                int win1I = GetIndex();
                var win1av = new List<string> { $"119,0,{houseindex},0,0,0,0,A" };
                var win1action = new Action(win1I, win1av);
                var win1ev = new List<string> { $"13,0,{startTime + winTime}" };
                var win1event = new Event(win1I, win1ev);
                var win1trigger = new Trigger(win1I, "destroy enemy");
                var win1full = new FullTrigger(win1action, win1event, win1trigger, 0);
                win1full.WriteInIni();

                int win2I = GetIndex();
                var win2av = new List<string> { $"2,0,{houseindex},0,0,0,0,A" };
                var win2action = new Action(win2I, win2av);
                var win2ev = new List<string> { $"13,0,{startTime + winTime + 10}" };
                var win2event = new Event(win2I, win2ev);
                var win2trigger = new Trigger(win2I, "forcefully end game");
                var win2full = new FullTrigger(win2action, win2event, win2trigger, 0);
                win2full.WriteInIni();
            }
            if (File.Exists("output.ini"))
                File.Delete("output.ini");
            Output.WriteIniFile("output.ini");
        }
    }
}


/*[General]
AttackHouse=<Player @ A>  ;进攻国家
InitialSequenceNumber=10000   ;注册表的开始序号，本程序生成的格式为0010001，与FA2不同
WinTime=3600 ;胜利时间，-1为无限

[Attack1]
StartTime=120   ;触发激活的时间，注意实际刷兵开始的时间为StartTime+CycleTime
CycleTime=60    ;触发循环的时间
RepeatTimes=10  ;触发重复的次数，1为不重复，0为无限
TaskForce=5,SREF/6,BFRT ;作战小队，不超过6种单位
StartWaypoint=0 ;刷兵的路径点
VeteranLevel=2  ;经验等级，123为新兵、老兵、精英
AvoidThreats=false; ;是否躲避威胁
Suicide=false;      ;是否为自杀性攻击
Droppod=yes     ;是否为空降部队
Chronosphere=false; ;是否为超时空部队，覆盖上一条
Full=false;         ;是否为预装载部队
AreTeamMembersRecruitable=false; ;小队成员是否可以被重组
RecruitFromExistingUnits=false;  ;是否直接重组已有单位（优先重组接近StartWaypoint的）
Script=3,9/3,10/0,0          ;脚本，不超过50个
*/
