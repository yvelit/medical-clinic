using Domain.People;

namespace Domain.UnitTest.People.Fakes
{
    public class FakeCode : Code
    {
        public FakeCode(string value) : base(value)
        {
        }

        public static explicit operator FakeCode(string value)
        {
            return new FakeCode(value);
        }
    }
}
