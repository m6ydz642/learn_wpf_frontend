using System.ComponentModel;

namespace TestProject
{
    /// <summary>
    /// 태스크
    /// </summary>
    public class Task : INotifyPropertyChanged
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Event
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 속성 변경시 - PropertyChanged

        /// <summary>
        /// 속성 변경시
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 명칭
        /// </summary>
        private string name;

        /// <summary>
        /// 설명
        /// </summary>
        private string description;

        /// <summary>
        /// 우선 순위
        /// </summary>
        private int priority;

        /// <summary>
        /// 태스크 타입
        /// </summary>
        private TaskType taskType;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 태스크명 - TaskName

        /// <summary>
        /// 태스크명
        /// </summary>
        public string TaskName
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;

                FirePropertyChangedEvent(nameof(TaskName));
            }
        }

        #endregion
        #region 설명 - Description

        /// <summary>
        /// 설명
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;

                FirePropertyChangedEvent(nameof(Description));
            }
        }

        #endregion
        #region 우선 순위 - Priority

        /// <summary>
        /// 우선 순위
        /// </summary>
        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;

                FirePropertyChangedEvent(nameof(Priority));
            }
        }

        #endregion
        #region 태스크 타입 - TaskType

        /// <summary>
        /// 태스크 타입
        /// </summary>
        public TaskType TaskType
        {
            get
            {
                return this.taskType;
            }
            set
            {
                this.taskType = value;

                FirePropertyChangedEvent(nameof(TaskType));
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - Task()

        /// <summary>
        /// 생성자
        /// </summary>
        public Task()
        {
        }

        #endregion
        #region 생성자 - Task(name, description, priority, taskType)

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="description">설명</param>
        /// <param name="priority">우선 순위</param>
        /// <param name="taskType">태스트 타입</param>
        public Task(string name, string description, int priority, TaskType taskType)
        {
            this.name        = name;
            this.description = description;
            this.priority    = priority;
            this.taskType    = taskType;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 문자열 구하기 - ToString()

        /// <summary>
        /// 문자열 구하기
        /// </summary>
        /// <returns>문자열</returns>
        public override string ToString()
        {
            return this.name.ToString();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Protected

        #region 속성 변경시 이벤트 발생시키기 - FirePropertyChangedEvent(propertyName)

        /// <summary>
        /// 속성 변경시 이벤트 발생시키기
        /// </summary>
        /// <param name="propertyName">속성명</param>
        protected void FirePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}