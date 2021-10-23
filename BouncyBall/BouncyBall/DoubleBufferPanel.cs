﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBall
{
    class DoubleBufferPanel : Panel

    {

        public DoubleBufferPanel()

        {

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true
                );
    
            this.UpdateStyles();

        }

    }
}
