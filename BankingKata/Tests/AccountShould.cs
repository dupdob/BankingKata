using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace BankingKata.Tests
{
    [TestFixture]
    class AccountShould
    {
        [Test]
        public void
            SupportDemoUseCase()
        {
            // Given
            var myAccount = new Account();

            // When
            myAccount.Deposit(1000, "01/04/2014");
            myAccount.Withdraw(100, "02/04/2014");
            myAccount.Deposit(500, "10/04/2014");
            var recorder = new ConsoleRecorder();
            using (recorder)
            {
                myAccount.PrintStatement();
            }

            // Then
            // console should display
            Check.That(recorder.Display).AsLines().ContainsExactly(
                "DATE | AMOUNT | BALANCE",
                "10/04/2014 | 500,00 | 1400,00",
                "02/04/2014 | -100 | 900,00",
                "01/04/2014 | 1000,00 | 1000,00");

        }

        [Test]
        public void
            SupportSimpleDeposit()
        {
            // Given
            var myAccount = new Account();

            // When
            myAccount.Deposit(1000, "01/04/2014");
            var recorder = new ConsoleRecorder();
            using (recorder)
            {
                myAccount.PrintStatement();
            }

            // Then
            // console should display
            Check.That(recorder.Display).AsLines().ContainsExactly(
                "DATE | AMOUNT | BALANCE",
                "01/04/2014 | 1000,00 | 1000,00");
        }


        [Test]
        public void
            PrintHeadersWhenNoOperation()
        {
            // given
            var myAccount = new Account();

            var recorder = new ConsoleRecorder();
            using (recorder)
            {
                // when
                myAccount.PrintStatement();                
            }

            // Then console should display
            // DATE | AMOUNT | BALANCE

            Check.That(recorder.Display).AsLines().ContainsExactly("DATE | AMOUNT | BALANCE");
        }
    }

    // capture console display with using pattern
    internal class ConsoleRecorder : IDisposable
    {
        private readonly TextWriter oldOutStream;
        private readonly StringWriter recorder;

        public string Display => recorder.ToString();

        public ConsoleRecorder()
        {
            this.oldOutStream = Console.Out;
            this.recorder = new StringWriter();
            Console.SetOut(recorder);
        }

        public void Dispose()
        {
            Console.SetOut(this.oldOutStream);
        }
    }
}
