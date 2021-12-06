using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolBox.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<string, string>> personJobs = new List<Tuple<string, string>>();
            using (StreamReader sr = new StreamReader("d:\\PersonId_JobId.txt"))
            {
                string line = sr.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    string[] values = line.Split(',');
                    string _personId = values[0];
                    string _jobId = values[1];
                    personJobs.Add(new Tuple<string, string>(_personId, _jobId));
                    line = sr.ReadLine();
                }

            }

            //for (int i = 0; i < personJobs.Count; i++)
            //{
            //    Test2(personJobs[i].Item1, personJobs[i].Item2);
            //}

            Task[] tasks = new Task[personJobs.Count];
            TaskFactory taskFactory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(8));
            for (int i = 0; i < personJobs.Count; i++)
            {
                string _personId = personJobs[i].Item1;
                string jobId = personJobs[i].Item2;
                tasks[i] = taskFactory.StartNew(() =>
                { //单租户修复
                    Test2(_personId, jobId);
                });
            }

            Task.WaitAll(tasks); //等待完成
            System.Console.WriteLine("All complete");
            System.Console.ReadLine();
        }

        private static string Test2(string personId, string jobId)
        {
            string cookie = "Hm_lvt_51459645fe0ee98a62e88b9592538957=1636632281; Hm_lvt_ccc1fca4a4cd9ab2d76aede40acc4e31=1636632249,1638343125,1638683241; BSUSER=NjM3NzQ0MDY0Nzk4OTY4MzI4LjEwMDEwMi4xMTU1NDE4NTM=; ssn_BSUSER=NjM3NzQ0MDY0Nzk4OTY4MzI4LjEwMDEwMi4xMTU1NDE4NTM=; Hm_lpvt_ccc1fca4a4cd9ab2d76aede40acc4e31=1638780884; BSSaaS=0102F65CCCDB98B8D908FEF690AE0C9DB8D90800423100310035003500340031003800350033005F00660031003900610034003200310036002D0066006300380037002D0034003300650032002D0039003700640065002D003000340030003900610061003700330037006500610031005F003100300030003100300032005F00310030002E003100320039002E0033002E0033005F003000423100310035003500340031003800350033005F00660031003900610034003200310036002D0066006300380037002D0034003300650032002D0039003700640065002D003000340030003900610061003700330037006500610031005F003100300030003100300032005F00310030002E003100320039002E0033002E0033005F003000012F00FF; ssn_BSSaaS=0102F65CCCDB98B8D908FEF690AE0C9DB8D90800423100310035003500340031003800350033005F00660031003900610034003200310036002D0066006300380037002D0034003300650032002D0039003700640065002D003000340030003900610061003700330037006500610031005F003100300030003100300032005F00310030002E003100320039002E0033002E0033005F003000423100310035003500340031003800350033005F00660031003900610034003200310036002D0066006300380037002D0034003300650032002D0039003700640065002D003000340030003900610061003700330037006500610031005F003100300030003100300032005F00310030002E003100320039002E0033002E0033005F003000012F00FF";
            string url = $"https://recruitv5.tms.beisen.net/Recruiting/Applicant/Overview/BasicInfo/Get?personId={personId}&jobId={jobId}";

            var result = THttp.SimpleGetString(url, cookie);
            System.Console.WriteLine($"personId:{personId},jobId:{jobId} success");
            return result;
        }

        /// <summary>
        /// Provides a task scheduler that ensures a maximum concurrency level while
        /// running on top of the ThreadPool.
        /// </summary>
        public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
        {
            /// <summary>Whether the current thread is processing work items.</summary>
            [ThreadStatic]
            private static bool _currentThreadIsProcessingItems;
            /// <summary>The list of tasks to be executed.</summary>
            private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)
                                                                               /// <summary>The maximum concurrency level allowed by this scheduler.</summary>
            private readonly int _maxDegreeOfParallelism;
            /// <summary>Whether the scheduler is currently processing work items.</summary>
            private int _delegatesQueuedOrRunning = 0; // protected by lock(_tasks)

            /// <summary>
            /// Initializes an instance of the LimitedConcurrencyLevelTaskScheduler class with the
            /// specified degree of parallelism.
            /// </summary>
            /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism provided by this scheduler.</param>
            public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
            {
                if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
                _maxDegreeOfParallelism = maxDegreeOfParallelism;
            }

            /// <summary>Queues a task to the scheduler.</summary>
            /// <param name="task">The task to be queued.</param>
            protected sealed override void QueueTask(Task task)
            {
                // Add the task to the list of tasks to be processed.  If there aren't enough
                // delegates currently queued or running to process tasks, schedule another.
                lock (_tasks)
                {
                    _tasks.AddLast(task);
                    if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
                    {
                        ++_delegatesQueuedOrRunning;
                        NotifyThreadPoolOfPendingWork();
                    }
                }
            }

            /// <summary>
            /// Informs the ThreadPool that there's work to be executed for this scheduler.
            /// </summary>
            private void NotifyThreadPoolOfPendingWork()
            {
                ThreadPool.UnsafeQueueUserWorkItem(_ =>
                {
                    // Note that the current thread is now processing work items.
                    // This is necessary to enable inlining of tasks into this thread.
                    _currentThreadIsProcessingItems = true;
                    try
                    {
                        // Process all available items in the queue.
                        while (true)
                        {
                            Task item;
                            lock (_tasks)
                            {
                                // When there are no more items to be processed,
                                // note that we're done processing, and get out.
                                if (_tasks.Count == 0)
                                {
                                    --_delegatesQueuedOrRunning;
                                    break;
                                }

                                // Get the next item from the queue
                                item = _tasks.First.Value;
                                _tasks.RemoveFirst();
                            }

                            // Execute the task we pulled out of the queue
                            base.TryExecuteTask(item);
                        }
                    }
                    // We're done processing items on the current thread
                    finally { _currentThreadIsProcessingItems = false; }
                }, null);
            }

            /// <summary>Attempts to execute the specified task on the current thread.</summary>
            /// <param name="task">The task to be executed.</param>
            /// <param name="taskWasPreviouslyQueued"></param>
            /// <returns>Whether the task could be executed on the current thread.</returns>
            protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
            {
                // If this thread isn't already processing a task, we don't support inlining
                if (!_currentThreadIsProcessingItems) return false;

                // If the task was previously queued, remove it from the queue
                if (taskWasPreviouslyQueued) TryDequeue(task);

                // Try to run the task.
                return base.TryExecuteTask(task);
            }

            /// <summary>Attempts to remove a previously scheduled task from the scheduler.</summary>
            /// <param name="task">The task to be removed.</param>
            /// <returns>Whether the task could be found and removed.</returns>
            protected sealed override bool TryDequeue(Task task)
            {
                lock (_tasks) return _tasks.Remove(task);
            }

            /// <summary>Gets the maximum concurrency level supported by this scheduler.</summary>
            public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

            /// <summary>Gets an enumerable of the tasks currently scheduled on this scheduler.</summary>
            /// <returns>An enumerable of the tasks currently scheduled.</returns>
            protected sealed override IEnumerable<Task> GetScheduledTasks()
            {
                bool lockTaken = false;
                try
                {
                    Monitor.TryEnter(_tasks, ref lockTaken);
                    if (lockTaken) return _tasks.ToArray();
                    else throw new NotSupportedException();
                }
                finally
                {
                    if (lockTaken) Monitor.Exit(_tasks);
                }
            }
        }
    }
}
