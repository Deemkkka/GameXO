using System;
using System.Windows;


namespace GameXO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        ///<summary>
        ///текущие результаты ячеек в активной игре
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True - если ходит игрок 1 (Х) или игрок 2 (О)
        /// </summary>
        private bool mPlayerMove;
        
        /// <summary>
        /// True - если игра окончена
        /// </summary>
        private bool mGameEnded;


        #endregion

        #region Constructor



        public MainWindow()
        {
            InitializeComponent();

            Game();
        }



        #endregion
        
        
        private void Game()
        {
            //массив свободных клеток
            mResults = new MarkType[9];
        }


    }
}
