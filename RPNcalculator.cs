using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorOnWPF
{
    class Token
    {

    }
    
    class Number : Token
    {
        double value;
        public Number(double value)
        {
            this.value = value;
        }
    }

    class Parenthesis : Token
    {
        bool isOpening;
        public Parenthesis(bool isOpening)
        {
            this.isOpening = isOpening;
        }
    }

    class Operation : Token
    {
        string value;
        int priority;
        public Operation(string value, int priority)
        {
            this.value = value;
            this.priority = priority;
        }
    }

    class Function : Token
    {

    }

    class Variable : Token
    {

    }

    internal class RPNcalculator
    {
        static List<Token> tokenize(string input)
        { 
            List<Token> tokens = new List<Token>();
            
            return tokens;
        }
        
    }
}
