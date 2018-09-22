using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    public class TableInsertion
    {
        private string inRegionName;
        private string inShelvesName;
        private int outCount;

        public TableInsertion()
        {
        }

        public TableInsertion(string inRegionName, string inShelvesName, int outCount)
        {
            this.inRegionName = inRegionName;
            this.inShelvesName = inShelvesName;
            this.outCount = outCount;
        }

        public string InRegionName
        {
            get
            {
                return inRegionName;
            }

            set
            {
                inRegionName = value;
            }
        }

        public string InShelvesName
        {
            get
            {
                return inShelvesName;
            }

            set
            {
                inShelvesName = value;
            }
        }

        public int OutCount
        {
            get
            {
                return outCount;
            }

            set
            {
                outCount = value;
            }
        }
    }
}
