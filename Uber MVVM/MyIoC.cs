using System;
using Autofac;

namespace Uber_MVVM
{
    public class MyIoC
    {
        private static MyIoC _reference = new MyIoC();
        public static MyIoC Reference { get { return _reference; } }

        private IContainer container;
        private ContainerBuilder builder;

        private MyIoC()
        {         
            builder = new ContainerBuilder();
        }

        public MyIoC Register<TImplementation, TInterface>()
        {
            builder.RegisterType<TImplementation>().As<TInterface>();
            return Reference;
        }

        public MyIoC Register<TImplementation>()
        {
            builder.RegisterType<TImplementation>();
            return Reference;
        }

        public void Build()
        {
            container = builder.Build();
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }

}
