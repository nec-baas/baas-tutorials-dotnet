using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Nec.Nebula;
using tutorial1.Annotations;

namespace tutorial1
{
    /// <summary>
    /// Todo ViewModel
    /// </summary>
    class TodoViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// オブジェクトバケット
        /// </summary>
        private NbObjectBucket<NbObject> _bucket;
        
        /// <summary>
        /// 取得した Todo の NbObject 配列
        /// </summary>
        private IList<NbObject> _todos;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TodoViewModel()
        {
            _bucket = new NbObjectBucket<NbObject>("TodoTutorial1");
        }

        /// <summary>
        /// ListView に表示するテキスト
        /// </summary>
        public IEnumerable<string> TodoTexts
        {
            get
            {
                if (_todos == null) return null;
                return from x in _todos select x["description"] as string;
            }
        }

        /// <summary>
        /// サーバから Todo 一覧をリロードする
        /// </summary>
        public async void Reload()
        {
            var todos = await _bucket.QueryAsync(new NbQuery().OrderBy("updatedAt"));
            _todos = todos.ToList();

            OnPropertyChanged("TodoTexts");
        }

        /// <summary>
        /// Todo テキストを追加する
        /// </summary>
        /// <param name="text">テキスト</param>
        public async void Add(string text)
        {
            // NbObject 作成
            var obj = _bucket.NewObject();
            obj.Acl = NbAcl.CreateAclForAnonymous();
            obj["description"] = text;

            // 保存
            var result = await obj.SaveAsync();

            Reload();
        }

        /// <summary>
        /// 指定位置の Todo を削除する
        /// </summary>
        /// <param name="index">インデックス</param>
        public async void RemoveAt(int index)
        {
            await _todos[index].DeleteAsync();

            _todos.RemoveAt(index);
            OnPropertyChanged("TodoTexts");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
