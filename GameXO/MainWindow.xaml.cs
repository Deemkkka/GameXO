using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private bool mPlayer1Move;
        
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
        
        
        /// <summary>
        /// Стартовать новую игру и очистить значения обратно перед стартом
        /// </summary>
        private void Game()
        {
            //массив свободных клеток
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            //игрок1 начинает игру
            mPlayer1Move = true;

            // взаимодействовать с каждой кнопкой
            GridCells.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // изменить цвет фона, цвет контента и очистить контент
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //убедиться, что игра не закончена
            mGameEnded = false;
        }


        /// <summary>
        /// обработка события клика
        /// </summary> 
        /// <param name="sender">кнопка, которая была нажата</param>
        /// <param name="e">событие клика</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // начать новую игру при клике после окончания
            if (mGameEnded)
            {
                Game();
                return;
            }

            // передать отправителю кнопки
            var button = sender as Button;  // = (Button)sender

            //найти позиции кнопки в массиве
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //ничего не делаем, если уже есть значение
            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            //установить значение ячейки, который выполняет игрок

            mResults[index] = mPlayer1Move ? MarkType.Cross : MarkType.Nought;

            //if (mPlayer1Move)
            //{
            //    mResults[index] = MarkType.Cross;
            //}
            //else
            //{
            //    mResults[index] = MarkType.Nought; 
            //}

            //присваиваем значение 
            button.Content = mPlayer1Move ? "X" : "O";

            //переключить игрока
            if (mPlayer1Move)
                mPlayer1Move = false;
            else
                mPlayer1Move = true;
                
            
            
        }
    }
}
