﻿using Strategy_game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    class BuildArmConcrete : BuildpartBuilder
    {
        public override void BuildPart()
        {
            Arm a = new Arm();
            a.HealthModify = 0;
            a.NameModify = "";
        }
    }
}