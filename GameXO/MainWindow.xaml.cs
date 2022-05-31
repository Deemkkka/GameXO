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

            //изменяем цвет второго игрока на красный
            if (!mPlayer1Move)
            {
                button.Foreground = Brushes.Red;
            }

            //переключить игрока
            if (mPlayer1Move)
                mPlayer1Move = false;
            else
                mPlayer1Move = true;

            CheckWin();
            
        }

        /// <summary>
        /// проверка, кто победил
        /// </summary>
        private void CheckWin()
        {
            // проверка с 0 по 2 индекс (верхние 3 клетки) 
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // игра окончена
                mGameEnded = true;
                // выделяем фон зеленым
                btn0_0.Background = btn1_0.Background = btn2_0.Background = Brushes.DarkGreen; 
                
            }
            else if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                btn0_1.Background = btn1_1.Background = btn2_1.Background = Brushes.DarkGreen;
            }
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                btn0_2.Background = btn1_2.Background = btn2_2.Background = Brushes.DarkGreen;
            }
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                btn0_0.Background = btn0_1.Background = btn0_2.Background = Brushes.DarkGreen;
            }
            else if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                btn0_1.Background = btn1_1.Background = btn2_1.Background = Brushes.DarkGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                btn0_2.Background = btn1_2.Background = btn2_2.Background = Brushes.DarkGreen;
            }
            else if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                btn0_0.Background = btn1_1.Background = btn2_2.Background = Brushes.DarkGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                btn0_2.Background = btn1_1.Background = btn2_0.Background = Brushes.DarkGreen;
            }
        }
    }
}
