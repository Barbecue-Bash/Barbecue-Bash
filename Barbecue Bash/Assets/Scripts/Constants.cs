using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public static class Constants
    {
        public static int Rows = ShapesManager.Rows;
        public static int Columns = 8;

        public static int Match3Score = 50;
        public static int SubsequentMatchScore = 1000;

        public static readonly float AnimationDuration =  0.2f;

        public static readonly float MoveAnimationMinDuration = 0.05f;

        public static readonly float ExplosionDuration = 0.3f;

        public static readonly float WaitBeforePotentialMatchesCheck = 2f;
        public static readonly float OpacityAnimationFrameDelay = 0.05f;

        public static readonly int MinimumMatches = 3;
        public static readonly int MinimumMatchesForBonus = 4;

        public static int[] topScores = {0, 0, 0, 0, 0};
    }
