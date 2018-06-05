using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BankingKata
{
    internal class Account
    {
        private IList<Operation> operations = new List<Operation>();
        private bool depositDone = false;

        public void Deposit(double amount, String date)
        {
            this.operations.Add(new Operation(amount, date));
            this.depositDone = true;
        }

        public void Withdraw(double amount, String date)
        {
            this.operations.Add(new Operation(-amount, date));
            this.depositDone = true;
        }

        public void PrintStatement()
        {
            Console.WriteLine("DATE | AMOUNT | BALANCE");
            if (this.depositDone)
            {
                var report = new StringBuilder();
                double balance = 0;
                foreach (var operation in operations)
                {
                    balance += operation.Amount;
                    if (operation.Amount>=0)
                    {
                        report.Insert(0, 
                        string.Format("{0} | {1:F2} | {2:F2}"+Environment.NewLine, 
                        operation.Date, 
                        operation.Amount, 
                        balance));
                    }
                    else
                    {
                        // TODO: confirm with PO if this is expected format
                        report.Insert(0, 
                            string.Format("{0} | {1} | {2:F2}"+Environment.NewLine, 
                                operation.Date, 
                                operation.Amount, 
                                balance));
                    }
                }

                Console.Write(report.ToString());
            }
        }

        struct Operation
        {
            public double Amount;
            public string Date;

            public Operation(double amount, string date)
            {
                Amount = amount;
                Date = date;
            }
        }
    }
}