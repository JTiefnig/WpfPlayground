﻿using System;


namespace XGraphTest
{
    class SampleClass
    {
        public int OutputPortAsInt { get { return 0; } }

        public int BiPortAsInt { get; set; }

        public int InputPortAsInt { set { } }

        public event Action<int> TimeChanged;
    }

    class SampleClass1VeryTooMuchLong
    {
        public int OutputPortAsInt { get { return 0; } }

        public int BiPortAsInt { get; set; }

        public int InputPortAsInt { set { } }

        public event Action<int> TimeChanged;
    }

}