using System.Collections.Generic;

namespace Algorithm
{
    public class Finder
    {
        private readonly List<Thing> _p;

        public Finder(List<Thing> p)
        {
            _p = p;
        }
        
        public F Find(FT ft)
        {
            var tr = PopulateTR();

            if(tr.Count < 1)
            {
                return new F();
            }

            return AnswerCalculator.CalculateAnswer(ft, tr);
        }



        private List<F> PopulateTR()
        {
            var tr = new List<F>();

            for (var i = 0; i < _p.Count - 1; i++)
            {
                for (var j = i + 1; j < _p.Count; j++)
                {
                    var r = new F();
                    if (_p[i].BirthDate < _p[j].BirthDate)
                    {
                        r.P1 = _p[i];
                        r.P2 = _p[j];
                    }
                    else
                    {
                        r.P1 = _p[j];
                        r.P2 = _p[i];
                    }
                    r.D = r.P2.BirthDate - r.P1.BirthDate;
                    tr.Add(r);
                }
            }
            return tr;
        }
    }

    public class AnswerCalculator
    {
        public static F CalculateAnswer(FT ft, List<F> tr)
        {
            if (ft == FT.One)
                return calculateAnswerForOne(tr);
            if (ft == FT.Two)
            {
                return calculateAnswerForTwo(tr);
            }
            return null;
        }

        public static F calculateAnswerForOne(List<F> tr)
        {
            F answer = tr[0];
            foreach (var result in tr)
            {
                if (result.D < answer.D)
                {
                    answer = result;
                }
            }
            return answer;
        }

        public static F calculateAnswerForTwo(List<F> tr)
        {
            F answer = tr[0];
            foreach (var result in tr)
            {
                if (result.D > answer.D)
                {
                    answer = result;
                }
                //break;


            }
            return answer;
        }
    }
}