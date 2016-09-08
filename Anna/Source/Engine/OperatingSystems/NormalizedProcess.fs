﻿namespace Anna.Source.Engine.OperatingSystems

open System
open System.Drawing

[<CustomEquality; CustomComparison>]
type NormalizedProcess =
    {
        processId: Int32
        processName: String
        startTime: DateTime
        isSystemProcess: Boolean
        icon: Icon
    }

    override this.Equals(otherObject) =
           match otherObject with
             | :? NormalizedProcess as otherProcess -> (this.processId = otherProcess.processId)
             | _ -> false

    override this.GetHashCode() =
        hash(this.processId)

    /// <summary>
    /// Priority for sorting is as follows:
    /// - Sort by time since the processes started (newest first)
    ///     - Since this property is not accessable for system processes, sort those by id
    /// - Place system processes below standard processes
    /// </summary>
    interface System.IComparable with
        member this.CompareTo otherObject =
            match otherObject with
                | :? NormalizedProcess as otherProcess ->
                    if (this.isSystemProcess && not otherProcess.isSystemProcess) then 1
                    else if (otherProcess.isSystemProcess && not this.isSystemProcess) then -1
                    else if (not this.isSystemProcess && not otherProcess.isSystemProcess) then -this.startTime.CompareTo(otherProcess.startTime)
                    else this.processId.CompareTo(otherProcess.processId)
                | _ -> invalidArg "otherProcess" "Objects do not have the same type" 
(*

// Class that can sort processes by time since execution
class ProcessTimeComparer : IComparer<Process>
{
    public static readonly ProcessTimeComparer Default = new ProcessTimeComparer();
    public ProcessTimeComparer() { }

    public int Compare(Process ProcessA, Process ProcessB)
    {
        try
        {
            return DateTime.Compare(ProcessB.StartTime, ProcessA.StartTime);
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }
} // End class

class ProcessIDComparer : IComparer<Process>
{
    public static readonly ProcessIDComparer Default = new ProcessIDComparer();
    public ProcessIDComparer() { }

    public int Compare(Process ProcessA, Process ProcessB)
    {
        if (ProcessA.Id < ProcessB.Id)
            return 1;
        else if (ProcessA.Id > ProcessB.Id)
            return -1;
        else
            return 0;
    }
} // End class
*)