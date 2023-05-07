namespace Time_TimePeriod
{
    using System;
    #region Time
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte hours;
        private readonly byte minutes;
        private readonly byte seconds;

        public byte Hours => hours;
        public byte Minutes => minutes;
        public byte Seconds => seconds;

        public Time(byte hours, byte minutes = 0, byte seconds = 0)
        {
            if (hours > 23 || minutes > 59 || seconds > 59)
                throw new ArgumentException("Invalid time parameters");

            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public Time(string time)
        {
            if (!TryParse(time, out Time result))
                throw new ArgumentException("Invalid time string format");

            this.hours = result.hours;
            this.minutes = result.minutes;
            this.seconds = result.seconds;
        }

        public static bool TryParse(string time, out Time result)
        {
            result = default;
            if (string.IsNullOrEmpty(time))
                return false;

            string[] parts = time.Split(':');
            if (parts.Length != 3)
                return false;

            if (!byte.TryParse(parts[0], out byte hours) ||
                !byte.TryParse(parts[1], out byte minutes) ||
                !byte.TryParse(parts[2], out byte seconds) ||
                hours > 23 || minutes > 59 || seconds > 59)
                return false;

            result = new Time(hours, minutes, seconds);
            return true;
        }

        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj is Time);
        }

        public bool Equals(Time other)
        {
            return other != null && hours == other.hours && minutes == other.minutes && seconds == other.seconds;
        }

        public int CompareTo(Time other)
        {
            if (other == null)
                return 1;

            if (hours != other.hours)
                return hours.CompareTo(other.hours);

            if (minutes != other.minutes)
                return minutes.CompareTo(other.minutes);

            return seconds.CompareTo(other.seconds);
        }

        public static bool operator ==(Time time1, Time time2)
        {
            if (ReferenceEquals(time1, time2))
                return true;

            if (ReferenceEquals(time1, null) || ReferenceEquals(time2, null))
                return false;

            return time1.Equals(time2);
        }

        public static bool operator !=(Time time1, Time time2)
        {
            return !(time1 == time2);
        }

        public static bool operator <(Time time1, Time time2)
        {
            if (ReferenceEquals(time1, time2))
                return false;

            if (ReferenceEquals(time1, null))
                return true;

            return time1.CompareTo(time2) < 0;
        }

        public static bool operator <=(Time time1, Time time2)
        {
            if (ReferenceEquals(time1, time2))
                return true;

            if (ReferenceEquals(time1, null))
                return true;

            return time1.CompareTo(time2) <= 0;
        }

        public static bool operator >(Time time1, Time time2)
        {
            return !(time1 <= time2);
        }

        public static bool operator >=(Time time1, Time time2)
        {
            return !(time1 < time2);
        }


        public Time Plus(TimePeriod timePeriod)
        {
            int totalSeconds = Hours * 3600 + Minutes * 60 + Seconds + timePeriod.TotalSeconds;
            totalSeconds %= 86400; // modulo 24 godziny
            byte hours = (byte)(totalSeconds / 3600);
            byte minutes = (byte)((totalSeconds / 60) % 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new Time(hours, minutes, seconds);
        }

        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        public static Time operator +(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        public Time Minus(TimePeriod timePeriod)
        {
            int totalSeconds = Hours * 3600 + Minutes * 60 + Seconds - timePeriod.TotalSeconds;
            totalSeconds = (totalSeconds + 86400) % 86400; // modulo 24 godziny
            byte hours = (byte)(totalSeconds / 3600);
            byte minutes = (byte)((totalSeconds / 60) % 60);
            byte seconds = (byte)(totalSeconds % 60);

            return new Time(hours, minutes, seconds);
        }

        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            return time.Minus(timePeriod);
        }

        public static Time operator -(Time time, TimePeriod timePeriod)
        {
            return time.Minus(timePeriod);
        }
    }
    #endregion
    #region TimePeriod
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly int totalSeconds;

        public int TotalSeconds => totalSeconds;

        public int Hours => totalSeconds / 3600;

        public int Minutes => (totalSeconds % 3600) / 60;

        public int Seconds => totalSeconds % 60;

        public TimePeriod(int hours = 0, int minutes = 0, int seconds = 0)
        {
            if (hours < 0 && minutes < 0 && seconds < 0)
                throw new ArgumentException("Time period values cannot be negative");

            totalSeconds = hours * 3600 + minutes * 60 + seconds;
        }

        public static bool TryParse(string timePeriod, out TimePeriod result)
        {
            result = default;
            if (string.IsNullOrEmpty(timePeriod))
                return false;

            string[] parts = timePeriod.Split(':');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out int hours) ||
                !int.TryParse(parts[1], out int minutes) ||
                !int.TryParse(parts[2], out int seconds) ||
                hours < 0 || minutes < 0 || seconds < 0)
                return false;

            result = new TimePeriod(hours, minutes, seconds);
            return true;
        }

        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj is TimePeriod);
        }

        public bool Equals(TimePeriod other)
        {
            return other != null && totalSeconds == other.totalSeconds;
        }

        public int CompareTo(TimePeriod other)
        {
            if (other == null)
                return 1;

            return totalSeconds.CompareTo(other.totalSeconds);
        }

        public static bool operator ==(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (ReferenceEquals(timePeriod1, timePeriod2))
                return true;

            if (ReferenceEquals(timePeriod1, null) || ReferenceEquals(timePeriod2, null))
                return false;

            return timePeriod1.Equals(timePeriod2);
        }

        public static bool operator !=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return !(timePeriod1 == timePeriod2);
        }

        public static bool operator <(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (ReferenceEquals(timePeriod1, timePeriod2))
                return false;

            if (ReferenceEquals(timePeriod1, null))
                return true;

            return timePeriod1.CompareTo(timePeriod2) < 0;
        }

        public static bool operator <=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (ReferenceEquals(timePeriod1, timePeriod2))
                return true;

            if (ReferenceEquals(timePeriod1, null))
                return true;

            return timePeriod1.CompareTo(timePeriod2) <= 0;
        }

        public static bool operator >(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return !(timePeriod1 <= timePeriod2);
        }

        public static bool operator >=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return !(timePeriod1 < timePeriod2);
        }


        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2)
        {
            long sum = t1.TotalSeconds + t2.TotalSeconds;
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2)
        {
            long sum = t1.TotalSeconds - t2.TotalSeconds;
            if (sum < 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod Multiply(TimePeriod t, double multiplier)
        {
            double sum = t.TotalSeconds * multiplier;
            if (sum < 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod Divide(TimePeriod t, double divisor)
        {
            double sum = t.TotalSeconds / divisor;
            if (sum < 0 || divisor == 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2)
        {
            long sum = t1.TotalSeconds + t2.TotalSeconds;
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2)
        {
            long sum = t1.TotalSeconds - t2.TotalSeconds;
            if (sum < 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod operator *(TimePeriod t, double multiplier)
        {

            double sum = t.TotalSeconds * multiplier;
            if (sum < 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }

        public static TimePeriod operator /(TimePeriod t, double divisor)
        {
            double sum = t.TotalSeconds / divisor;
            if (sum < 0 || divisor == 0) throw new ArgumentException("Invalid time parameters");
            return new TimePeriod((byte)((sum / 3600) % 24), (byte)((sum / 60) % 60), (byte)(sum % 60));
        }
    }
    #endregion
}