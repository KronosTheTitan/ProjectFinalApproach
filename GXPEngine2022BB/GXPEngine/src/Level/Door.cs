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

        public Door()
        {
            line = new Line(32 * 2.1f * 14, 32 * 2.1f * 2, 32 * 2.1f * 14, 32 * 4 * 2.1f);

            Level.lines.Add(line);
        }
    }
}
