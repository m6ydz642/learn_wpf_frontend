using System.Collections.ObjectModel;

namespace TestProject
{
    /// <summary>
    /// 태스크 컬렉션
    /// </summary>
    public class TaskCollection : ObservableCollection<Task>
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - TaskCollection()

        /// <summary>
        /// 생성자
        /// </summary>
        public TaskCollection() : base()
        {
            Add(new Task("Shoppingaaaaaaaa" , "Pick up Groceries and Detergent", 2, TaskType.Home));
            Add(new Task("Laundry"  , "Do my Laundry"                  , 2, TaskType.Home));
            Add(new Task("Email"    , "Email clients"                  , 1, TaskType.Work));
            Add(new Task("Clean"    , "Clean my office"                , 3, TaskType.Work));
            Add(new Task("Dinner"   , "Get ready for family reunion"   , 1, TaskType.Home));
            Add(new Task("Proposals", "Review new budget proposals"    , 2, TaskType.Work));
        }

        #endregion
    }
}