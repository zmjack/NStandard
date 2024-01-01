using System;
using System.Collections.Generic;
using Xunit;

namespace NStandard.Threading.Test;

public class CronTests
{
    [Fact]
    public void AnyTest()
    {
        var cron = new Cron { };
        Assert.Equal(new DateTime(2020, 1, 1, 1, 1, 59), cron.GetNextTime(new DateTime(2020, 1, 1, 1, 1, 58)));
        Assert.Equal(new DateTime(2020, 1, 1, 1, 2, 0), cron.GetNextTime(new DateTime(2020, 1, 1, 1, 1, 59)));

        Assert.Equal(new DateTime(2020, 1, 1, 1, 59, 0), cron.GetNextTime(new DateTime(2020, 1, 1, 1, 58, 59)));
        Assert.Equal(new DateTime(2020, 1, 1, 2, 0, 0), cron.GetNextTime(new DateTime(2020, 1, 1, 1, 59, 59)));

        Assert.Equal(new DateTime(2020, 1, 1, 23, 0, 0), cron.GetNextTime(new DateTime(2020, 1, 1, 22, 59, 59)));
        Assert.Equal(new DateTime(2020, 1, 2, 0, 0, 0), cron.GetNextTime(new DateTime(2020, 1, 1, 23, 59, 59)));

        Assert.Equal(new DateTime(2020, 1, 31, 0, 0, 0), cron.GetNextTime(new DateTime(2020, 1, 30, 23, 59, 59)));
        Assert.Equal(new DateTime(2020, 2, 1, 0, 0, 0), cron.GetNextTime(new DateTime(2020, 1, 31, 23, 59, 59)));

        Assert.Equal(new DateTime(2020, 12, 31, 0, 0, 0), cron.GetNextTime(new DateTime(2020, 12, 30, 23, 59, 59)));
        Assert.Equal(new DateTime(2021, 1, 1, 0, 0, 0), cron.GetNextTime(new DateTime(2020, 12, 31, 23, 59, 59)));
    }

    [Fact]
    public void SpecifiedTest1()
    {
        var cron = new Cron
        {
            SecondType = CronFieldType.Specified,
            Seconds = [0],

            MinuteType = CronFieldType.Specified,
            Minutes = [0],

            HourType = CronFieldType.Specified,
            Hours = [0],

            DayType = CronFieldType.Specified,
            Days = [31],
        };

        Assert.Equal(new DateTime(2020, 1, 31), cron.GetNextTime(new DateTime(2020, 1, 30)));
        Assert.Equal(new DateTime(2020, 3, 31), cron.GetNextTime(new DateTime(2020, 1, 31)));
        Assert.Equal(new DateTime(2021, 1, 31), cron.GetNextTime(new DateTime(2020, 12, 31)));
    }

    [Fact]
    public void SpecifiedTests2()
    {
        var cron = new Cron
        {
            SecondType = CronFieldType.Specified,
            Seconds = [0],

            MinuteType = CronFieldType.Specified,
            Minutes = [0],

            HourType = CronFieldType.Specified,
            Hours = [0],

            DayType = CronFieldType.Specified,
            Days = [31],
        };

        var results = GetNextTimes(cron, new DateTime(2020, 1, 30), 8);
        Assert.Equal(
        [
            new DateTime(2020, 1, 31),
            new DateTime(2020, 3, 31),
            new DateTime(2020, 5, 31),
            new DateTime(2020, 7, 31),
            new DateTime(2020, 8, 31),
            new DateTime(2020, 10, 31),
            new DateTime(2020, 12, 31),
            new DateTime(2021, 1, 31),
        ], results);
    }

    [Fact]
    public void SpecifiedTests3()
    {
        var cron = new Cron
        {
            SecondType = CronFieldType.Specified,
            Seconds = [0],

            MinuteType = CronFieldType.Specified,
            Minutes = [0],

            HourType = CronFieldType.Specified,
            Hours = [0],

            DayType = CronFieldType.Specified,
            Days = [29, 31],

            MonthType = CronFieldType.Specified,
            Months = [2, 3],
        };

        var results = GetNextTimes(cron, new DateTime(2020, 1, 30), 8);
        //Error
    }

    private static DateTime?[] GetNextTimes(Cron cron, DateTime time, int count = 1)
    {
        DateTime? _time = time;
        var list = new List<DateTime?>();

        for (int i = 0; i < count; i++)
        {
            if (_time != null)
            {
                _time = cron.GetNextTime(_time.Value);
            }
            list.Add(_time);
        }

        return list.ToArray();
    }

}
