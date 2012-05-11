using System.Collections.Generic;

namespace Algorithm
{
    public class Finder
    {
        private readonly List<Person> _people;

        public Finder(List<Person> people)
        {
            _people = people;
        }

        public PersonRelationship FindRelationship(FT ft)
        {
            var tr = GenerateTRList();

            if (tr.Count == 0)
            {
                return new PersonRelationship();
            }

            return new AnswerCalculator().CalculateAnswer(ft, tr);
        }

        private List<PersonRelationship> GenerateTRList()
        {
            var tr = new PeopleBuilder(_people).GeneratePeopleRelationships();
            return tr;
        }
    }

    public class PeopleBuilder
    {
        public List<Person> People { get; private set; }
 
        public PeopleBuilder(List<Person> people )
        {
            People = people;
        }

        public List<PersonRelationship> GeneratePeopleRelationships()
        {
            var tr = new List<PersonRelationship>();

            for (var i = 0; i < People.Count - 1; i++)
            {
                for (var j = i + 1; j < People.Count; j++)
                {
                    var r = PersonRelationship.CreatePair(People, j, i);
                    tr.Add(r);
                }
            }
            return tr;
        }
    }

    public abstract class AnswerHandler
    {
        public abstract PersonRelationship execute(List<PersonRelationship> tr);
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

        public override PersonRelationship execute(List<PersonRelationship> tr)
        {
            PersonRelationship answer = tr[0];
            foreach (var result in tr)
            {
                if (result.BirthdayDateDifference > answer.BirthdayDateDifference)
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

        public override PersonRelationship execute(List<PersonRelationship> tr)
        {
            PersonRelationship answer = tr[0];
            foreach (var result in tr)
            {
                if (result.BirthdayDateDifference < answer.BirthdayDateDifference)
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
 
        public PersonRelationship CalculateAnswer(FT ft, List<PersonRelationship> tr)
        {
            AnswerHandler handler = handlers[ft];
            return handler.execute(tr);
        }
    }
}