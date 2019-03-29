using System.Collections;
using System.Collections.Generic;

namespace PDollarGestureRecognizer {

    public class BestGesture {
        public string Name;
        public double Score;

        public BestGesture(string name, double score)
        {
            this.Name = name;
            this.Score = score;
        }
    }

}
