// See https://aka.ms/new-console-template for more information
using Time_TimePeriod;

TimePeriod t1 = new TimePeriod(5, 30, 0);
TimePeriod t2 = new TimePeriod(2, 45, 0);

TimePeriod t3 = TimePeriod.Plus(t1, t2);
TimePeriod t4 = TimePeriod.Minus(t1, t2);
TimePeriod t5 = TimePeriod.Multiply(t1, 2);
TimePeriod t6 = TimePeriod.Divide(t1, 2);

Console.WriteLine("t1: " + t1);
Console.WriteLine("t2: " + t2);
Console.WriteLine("t1 + t2 = " + t3);
Console.WriteLine("t1 - t2 = " + t4);
Console.WriteLine("t1 * 2 = " + t5);
Console.WriteLine("t1 / 2 = " + t6);
