using System;
using System.Text;
using System.Collections.Generic;
using Hotel.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;

namespace Hotel.Tests.Services
{
    public class A { }
    /// <summary>
    /// Сводное описание для BookingServiceUnitTest
    /// </summary>
    [TestClass]
    public class BookingServiceUnitTest
    {
        IBookingPageService BookingService;
        public BookingServiceUnitTest()
        {
            BookingService = new BookingService();
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test()
        {
            //Mock<IUoW<ApplicationDbContext>> mock = new Mock<IUoW<ApplicationDbContext>();
            Mock<A> mock = new Mock<A>();
        }
    }
}

