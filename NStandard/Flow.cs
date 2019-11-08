using System;

namespace NStandard
{
    public interface IFlow<T, TRet>
    {
        TRet Execute(T origin);
    }
    public class Flow<T, TRet> : IFlow<T, TRet>
    {
        public Func<T, TRet> Ret { get; }

        public Flow(Func<T, TRet> ret)
        {
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(origin);
    }

    public class Flow<T, TStep1, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TRet> ret)
        {
            Step1 = step1;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step1(origin));
    }

    public class Flow<T, TStep1, TStep2, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TStep2> Step2 { get; }
        public Func<TStep2, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TStep2> step2, Func<TStep2, TRet> ret)
        {
            Step1 = step1;
            Step2 = step2;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step2(Step1(origin)));
    }

    public class Flow<T, TStep1, TStep2, TStep3, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TStep2> Step2 { get; }
        public Func<TStep2, TStep3> Step3 { get; }
        public Func<TStep3, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TStep2> step2, Func<TStep2, TStep3> step3, Func<TStep3, TRet> ret)
        {
            Step1 = step1;
            Step2 = step2;
            Step3 = step3;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step3(Step2(Step1(origin))));
    }

    public class Flow<T, TStep1, TStep2, TStep3, TStep4, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TStep2> Step2 { get; }
        public Func<TStep2, TStep3> Step3 { get; }
        public Func<TStep3, TStep4> Step4 { get; }
        public Func<TStep4, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TStep2> step2, Func<TStep2, TStep3> step3, Func<TStep3, TStep4> step4, Func<TStep4, TRet> ret)
        {
            Step1 = step1;
            Step2 = step2;
            Step3 = step3;
            Step4 = step4;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step4(Step3(Step2(Step1(origin)))));
    }

    public class Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TStep2> Step2 { get; }
        public Func<TStep2, TStep3> Step3 { get; }
        public Func<TStep3, TStep4> Step4 { get; }
        public Func<TStep4, TStep5> Step5 { get; }
        public Func<TStep5, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TStep2> step2, Func<TStep2, TStep3> step3, Func<TStep3, TStep4> step4, Func<TStep4, TStep5> step5, Func<TStep5, TRet> ret)
        {
            Step1 = step1;
            Step2 = step2;
            Step3 = step3;
            Step4 = step4;
            Step5 = step5;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step5(Step4(Step3(Step2(Step1(origin))))));
    }

    public class Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TRet> : IFlow<T, TRet>
    {
        public Func<T, TStep1> Step1 { get; }
        public Func<TStep1, TStep2> Step2 { get; }
        public Func<TStep2, TStep3> Step3 { get; }
        public Func<TStep3, TStep4> Step4 { get; }
        public Func<TStep4, TStep5> Step5 { get; }
        public Func<TStep5, TStep6> Step6 { get; }
        public Func<TStep6, TRet> Ret { get; }

        public Flow(Func<T, TStep1> step1, Func<TStep1, TStep2> step2, Func<TStep2, TStep3> step3, Func<TStep3, TStep4> step4, Func<TStep4, TStep5> step5, Func<TStep5, TStep6> step6, Func<TStep6, TRet> ret)
        {
            Step1 = step1;
            Step2 = step2;
            Step3 = step3;
            Step4 = step4;
            Step5 = step5;
            Step6 = step6;
            Ret = ret;
        }

        public TRet Execute(T origin) => Ret(Step6(Step5(Step4(Step3(Step2(Step1(origin)))))));
    }

}
