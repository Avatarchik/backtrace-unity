﻿using Backtrace.Unity;
using Backtrace.Unity.Model;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RateLimitTests
    {
        private BacktraceClient client;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);
            client = gameObject.AddComponent<BacktraceClient>();
            client.Configuration = new BacktraceConfiguration()
            {
                ServerUrl = "https://test.sp.backtrace.io:6097/",
                //backtrace configuration require 64 characters
                Token = "1234123412341234123412341234123412341234123412341234123412341234"
            };
            client.SetClientReportLimit(3);
            gameObject.SetActive(true);
        }

        [UnityTest]
        public IEnumerator TestReportLimit_InvalidReportNumber_IgnoreAdditionalReports()
        {
            int maximumNumberOfRetries = 0;
            client.RequestHandler = (string url, BacktraceData data) =>
             {
                 maximumNumberOfRetries++;
                 return new BacktraceResult();
             };
            for (int i = 0; i < 5; i++)
            {
                client.Send("test");
            }
            Assert.IsTrue(maximumNumberOfRetries == 3);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestReportLimit_ValidReportNumber_AddAllReports()
        {
            int maximumNumberOfRetries = 0;
            client.RequestHandler = (string url, BacktraceData data) =>
            {
                maximumNumberOfRetries++;
                return new BacktraceResult();
            };
            for (int i = 0; i < 2; i++)
            {
                client.Send("test");
            }
            Assert.IsTrue(maximumNumberOfRetries == 2);
            yield return null;
        }
    }
}
