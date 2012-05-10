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

            if (tr.Count < 1)
            {
                return new F();
            }

            return new AnswerCalculator().CalculateAnswer(ft, tr);
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

    public abstract class AnswerHandler
    {
        public abstract F execute(List<F> tr);
        public AnswerCalculator AnswerCalculator { get; private set; }

        public AnswerHandler(AnswerCalculator answerCalculator)
        {
            AnswerCalculator = answerCalculator;
        }
    }

    public class FTTwoAnswerHandler : AnswerHandler
    {
        public FTTwoAnswerHandler(AnswerCalculator answerCalculator) : base(answerCalculator)
        {
        }

        public override F execute(List<F> tr)
        {
            F answer = tr[0];
            foreach (var result in tr)
            {
                if (result.D > answer.D)
                {
                    answer = result;
                }
            }
            return answer;
        }
    }

    public class FTOneAnswerHandler : AnswerHandler
    {
        public FTOneAnswerHandler(AnswerCalculator answerCalculator) : base(answerCalculator)
        {
        }

        public override F execute(List<F> tr)
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
    }

    public class AnswerCalculator
    {
        private readonly Dictionary<FT, AnswerHandler> handlers;
        public AnswerCalculator()
        {
            handlers = new Dictionary<FT, AnswerHandler>
                           {{FT.One, new FTOneAnswerHandler(this)}, {FT.Two, new FTTwoAnswerHandler(this)}};
        }
 
        public F CalculateAnswer(FT ft, List<F> tr)
        {
            AnswerHandler handler = handlers[ft];
            return handler.execute(tr);
        }
    }
}