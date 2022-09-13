using helloserve.SePush.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Tests
{
    [TestClass]
    public class TimeStringSlotConverterTests
    {
        [TestMethod]
        public void ShouldParseTimeStrings()
        {
            //arrange
            string json = "{ \"schedule\":[ [], [ \"20:00-22:30\" ], [ \"12:00-14:30\", \"20:00-22:30\" ] ] }";

            //act
            var result = JsonSerializer.Deserialize<TestStructure>(json);

            //assert
            Assert.AreEqual(3, result.Schedule.Count);
            Assert.AreEqual(new TimeSpan(20, 0, 0), result.Schedule[1][0].Start);
            Assert.AreEqual(new TimeSpan(22, 30, 0), result.Schedule[1][0].End);
            Assert.AreEqual(new TimeSpan(12, 0, 0), result.Schedule[2][0].Start);
            Assert.AreEqual(new TimeSpan(14, 30, 0), result.Schedule[2][0].End);
            Assert.AreEqual(new TimeSpan(20, 0, 0), result.Schedule[2][1].Start);
            Assert.AreEqual(new TimeSpan(22, 30, 0), result.Schedule[2][1].End);
        }
    }

    internal class TestStructure
    {
        [JsonPropertyName("schedule")]
        public List<ScheduleDaySlots> Schedule { get; set; }
    }
}
