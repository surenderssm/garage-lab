using System;
using System.Threading.Tasks;

namespace code_reviews_experiments
{
    public class MyData
    {
        public int Value;
    }

    public struct MyData1
    {
        public int Value;
    }
    public class Entity
    {
        private MyData myData;
        public MyData Foo() => myData;

        private MyData1 myData1;
        public MyData1 Foo1() => myData1;

    }
    public class CaseTwo : BaseCase
    {
        public async Task ExecuteAsync()
        {
            Example();
            await DoWork();
        }

        public void Example()
        {
            var entity = new Entity();
            int sum = 0;
            MyData v = entity.Foo();
            sum += v.Value;

            MyData1 v1 = entity.Foo1();
            sum += v.Value;

        }
    }
}
