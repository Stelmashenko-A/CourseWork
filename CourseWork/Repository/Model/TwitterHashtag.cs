using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterHashtag
    {
        public TwitterHashtag(string text, int start, int end)
        {
            Text = text;
            Start = start;
            End = end;
        }

        public string Text { get; }
        public int Start { get; }
        public int End { get; }
    }
}