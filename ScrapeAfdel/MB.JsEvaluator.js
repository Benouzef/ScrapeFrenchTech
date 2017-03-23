package MB.JsEvaluator
{
    class Evaluator
    {
        public function Eval(expr : String) : String
        {
            return eval(expr, "unsafe");
        }
    }
}