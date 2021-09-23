using System;
using System.Threading.Tasks;

namespace Anshan.Worker
{
    public interface IWorkerProxy
    {
        Task Execute(string workerName,Func<Task> func);
    }
}