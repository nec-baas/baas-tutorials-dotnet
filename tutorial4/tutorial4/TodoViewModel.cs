using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Nec.Nebula;
using tutorial4.Annotations;

namespace tutorial4
{
    /// <summary>
    /// Todo ViewModel
    /// </summary>
    class TodoViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// オブジェクトバケット(オフライン)
        /// </summary>
        private NbOfflineObjectBucket<NbOfflineObject> _bucket;

        /// <summary>
        /// 取得した Todo の NbOfflineObject 配列
        /// </summary>
        private IList<NbOfflineObject> _todos;

        /// <summary>
        /// 同期マネージャ
        /// </summary>
        private NbObjectSyncManager _syncManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TodoViewModel()
        {
            _bucket = new NbOfflineObjectBucket<NbOfflineObject>("TodoTutorial1");

            // 同期マネージャを作成し同期範囲を設定
            _syncManager = new NbObjectSyncManager();
            _syncManager.SetSyncScope("TodoTutorial1", new NbQuery());
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
        /// ローカル DB から Todo 一覧をリロードする
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
            // NbOfflineObject 作成
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

        /// <summary>
        /// Todo を同期する
        /// </summary>
        public async void Sync()
        {
            await _syncManager.SyncBucketAsync("TodoTutorial1", NbObjectConflictResolver.PreferServerResolver);

            Reload();
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
