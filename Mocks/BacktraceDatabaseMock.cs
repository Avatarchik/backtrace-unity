﻿using UnityEngine;
using Backtrace.Unity;

public class BacktraceDatabaseMock : BacktraceDatabase
{
    /// <summary>
    /// Make sure we won't remove any file/directory from unit-test code.
    /// </summary>
    protected override void RemoveOrphaned()
    {
        Debug.Log("Removing old reports");
    }

    /// <summary>
    /// Make sure we don't store any data on hard drive.
    /// </summary>
    protected override void CreateDatabaseDirectory()
    {
        Debug.Log("Creating database directory");
    }
}