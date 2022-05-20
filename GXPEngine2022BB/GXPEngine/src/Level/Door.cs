using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Level
{
    public class Door
    {
        private bool _open = false;

        public bool open   // property
        {
            get { return _open; }   // get method
            set
            {
                _open = value;
                line.active = _open;
            }  // set method
        }

        Line line;

        public Door(Line l)
        {
            line = l;

            Level.lines.Add(line);
        }
    }
}
