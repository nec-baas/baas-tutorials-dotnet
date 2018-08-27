using Nec.Nebula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tutorial2
{
    class UserViewModel
    {
        /// <summary>
        /// オブジェクトバケット
        /// </summary>
        NbUser _user;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserViewModel()
        {
        }

        /// <summary>
        /// サインアップ
        /// </summary>
        /// <param name="mail">メールアドレス</param>
        /// <param name="pass">パスワード</param>
        public async Task<bool> Signup(string mail, string pass)
        {
            NbUser user = new NbUser();
            user.Email = mail;
            try
            {
                _user = await user.SignUpAsync(pass);
            }
            catch (Exception e)
            {
                return false;
            }

            if (_user == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="mail">メールアドレス</param>
        /// <param name="pass">パスワード</param>
        public async Task<bool> Login(string mail, string pass)
        {
            try
            {
                _user = await NbUser.LoginWithEmailAsync(mail, pass); 
            }
            catch (Exception e)
            {
                return false;
            }

            if (_user == null)
            {
                return false;
            }

            return true;
        }
    }
}
