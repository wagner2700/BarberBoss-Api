﻿using BarberBoss.Infraestructure.DataAcess;
using Moq;

namespace CommonTestsLibraries.Repositories
{
    public class UnitOfWorkBuilder
    {

        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object;
        }
    }
}
