using NUnit.Framework;
using System;

namespace IocContainer.Test
{
    class IocContainerTest
    {
        [Test]
        public void TestRegisterWithoutParams()
        {
            Container cont = new Container();
            bool result = cont.Register<IRepository, Repository>();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestRegisterWithParams()
        {
            Container cont = new Container();
            bool result = cont.Register<IRepository, Repository>(LifestyleType.Transient);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestRegisterWithTransient()
        {
            Container cont = new Container();
            cont.Register<IRepository, Repository>(LifestyleType.Transient);
            IRepository repo1 = cont.Resolve<IRepository>();
            IRepository repo2 = cont.Resolve<IRepository>();
            bool result = repo1 == repo2;
            Assert.AreNotEqual(true, result);
        }

        [Test]
        public void create_singleton_instance()
        {
            var cont = new Container();
            cont.Register<ITypeToResolveTester, ConcreteTypeTester>(LifestyleType.Singleton);
            var instance = cont.Resolve<ITypeToResolveTester>();

            Assert.That(cont.Resolve<ITypeToResolveTester>(), Is.SameAs(instance));
        }

        [Test]
        public void TestResolve()
        {
            Container cont = new Container();
            cont.Register<IRepository, Repository>();
            IRepository repo = cont.Resolve<IRepository>();
            string result = repo.ShowMessage("This is a test");
            StringAssert.AreEqualIgnoringCase("This is a test", result);
        }

        [Test]
        public void resolve_object_with_registered_constructor_parameters()
        {
            var cont = new Container();

            cont.Register<ITypeToResolveTester, ConcreteTypeTester>();
            cont.Register<ITypeToResolveWithConstructorParamsTester, ConcreteTypeWithConstructorParamsTester>();

            var instance = cont.Resolve<ITypeToResolveWithConstructorParamsTester>();

            Assert.That(instance, Is.InstanceOf(typeof(ConcreteTypeWithConstructorParamsTester)));
        }

        [Test]
        public void throw_exception_trying_to_resolve_object_with_unregistered_constructor_parameters()
        {
            var cont = new Container();
            Exception exception = null;
            
            cont.Register<ITypeToResolveWithConstructorParamsTester, ConcreteTypeWithConstructorParamsTester>();

            try
            {
                var instance = cont.Resolve<ITypeToResolveWithConstructorParamsTester>();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.That(exception, Is.InstanceOf(typeof(NotRegisteredException)));
        }
    }

    #region test objects
    public interface ITypeToResolveTester { }

    public class ConcreteTypeTester : ITypeToResolveTester
    {
    }

    public interface ITypeToResolveWithConstructorParamsTester
    {
    }

    public class ConcreteTypeWithConstructorParamsTester : ITypeToResolveWithConstructorParamsTester
    {
        public ConcreteTypeWithConstructorParamsTester(ITypeToResolveTester typeToResolveTester)
        {

        }
    }
    #endregion
}
