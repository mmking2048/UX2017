using System.Collections.Generic;

namespace UX2017.Models
{
    public class Output<T>
    {
        public Status Status { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public enum Frequency
    {
        Quarter,
        Annual
    }

    public enum IndexSymbol
    {
        ONE,
        IDX,
        SPX,
        IQY,
        RUI,
        IUX,
        RUA,
        IUXX,
        DOWC,
        DOWT,
        DOWU,
        DOWI,
        TXCX,
        JX,
        TXSX,
        TTCS,
        TOOC,
        TXTW
    }

    public enum EventType
    {
        split,
        dividend,
        earnings
    }

    public enum Category
    {
        None,
        stocks,
        futures,
        forex
    }

    public enum DisplayType
    {
        headline,
        preview,
        full
    }
}