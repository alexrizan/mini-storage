using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForStarStuff
{
    /// <summary>
    /// Пожалуйста, разместите ссылки на код (код должен открываться в браузере, без архивов) на GitHub.
    ///1. Вы собираетесь совершить долгое путешествие через множество населенных пунктов.Чтобы не запутаться, вы сделали карточки вашего путешествия.
    ///Каждая карточка содержит в себе пункт отправления и пункт назначения.
    ///Гарантируется, что если упорядочить эти карточки так, чтобы для каждой карточки в упорядоченном списке пункт назначения на ней
    ///совпадал с пунктом отправления в следующей карточке в списке, получится один список карточек без циклов и пропусков.
    ///Например, у нас есть карточки
    ///Мельбурн > Кельн
    ///Москва > Париж
    ///Кельн > Москва
    ///Если упорядочить их в соответствии с требованиями выше, то получится следующий список:
    ///Мельбурн > Кельн, Кельн > Москва, Москва > Париж
    ///
    ///Требуется:
    ///•
    ///Написать функцию, которая принимает набор неупорядоченных карточек и возвращает набор упорядоченных карточек в соответствии с требованиями выше, 
    ///то есть в возвращаемом из функции списке карточек для каждой карточки пункт назначения на ней должен совпадать с пунктом отправления на следующей карточке.
    ///•
    ///Дать оценку сложности получившегося алгоритма сортировки
    ///•
    ///Написать тесты
    ///Оценивается прежде всего читабельность кода.
    ///2. Есть таблица хранящая линии покупки: Sales: Id, ProductId, CustomerId, DateCreated.Мы хотим понять, через какие продукты клиенты «попадают» к нам в магазин. 
    ///Напишите запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента.
    /// </summary>
    /// 
    class Program
    {
        static void Main(string[] args)
        {

            //!!! Для упрощения восприятия тестирования и отладки заменил названия на числа 

            //Верные исходные данные
            List<Card> lcards = new List<Card> {
                                                 new Card {  DepCity = 7,  ArrivCity = 8  },
                                                 new Card {  DepCity = 10, ArrivCity = 11 },
                                                 new Card {  DepCity = 8,  ArrivCity = 10 },
                                                 new Card {  DepCity = 11, ArrivCity = 1 },
                                                 new Card {  DepCity = 1,  ArrivCity = 3  },
                                                 new Card {  DepCity = 3,  ArrivCity = 7  }
                                               };

            //Исходные данные с петлей
            //Возможны варианты с петлей в маршруте, аналогичыми петлями в петлях,
            //различные комбинации, чтобы их правильно встроить в общую колоду,
            //поэтому данный вопрос не рассматривал
            //привожу как возможность
            List<Card> lcards_with_loop = new List<Card> {
                                                         new Card {  DepCity = 7,  ArrivCity = 8  },
                                                         new Card {  DepCity = 10, ArrivCity = 11 },
                                                         new Card {  DepCity = 3,  ArrivCity = 7  },
                                                         new Card {  DepCity = 8,  ArrivCity = 10 },
                                                         new Card {  DepCity = 11, ArrivCity = 1  },
                                                         new Card {  DepCity = 1,  ArrivCity = 3  },
                                                         //петля в маршруте:
                                                         new Card {  DepCity = 10, ArrivCity = 4 },
                                                         new Card {  DepCity = 4,  ArrivCity = 5 },
                                                         new Card {  DepCity = 5,  ArrivCity = 10 }

                                                        };//

            //Исходные данные заложенные с ошибкой ( прерыванием маршрута )
            //при равной длине, к примеру, двух возможных маршрутов
            //проблему правильного выборра можно оставить только на пользователя
            List<Card> lcards_with_error = new List<Card> {
                                                 new Card {  DepCity = 20, ArrivCity = 4  },
                                                 new Card {  DepCity = 4,  ArrivCity = 17 },
                                                 new Card {  DepCity = 17, ArrivCity = 9  },
                                                 new Card {  DepCity = 7,  ArrivCity = 8  },
                                                 new Card {  DepCity = 10, ArrivCity = 11 },
                                                 new Card {  DepCity = 8,  ArrivCity = 10 },
                                                 new Card {  DepCity = 11, ArrivCity = 1  },
                                                 new Card {  DepCity = 1,  ArrivCity = 3  },
                                                 new Card {  DepCity = 3,  ArrivCity = 7  }
                                               };


            bool err = TravelerPathSort.SortCards(ref lcards);

            if (err) Console.WriteLine("Ошибка: Некоректные исходные данные");


            foreach (var card in lcards)
            {
                Console.WriteLine(card.DepCity + " " + card.ArrivCity);
            }
            Console.ReadKey();

        }  
    }

    struct Card
    {
        public int DepCity;     //отправление
        public int ArrivCity;   //прибытие
    }

    class TravelerPathSort
    {
        public static bool SortCards(ref List<Card> lDeck)
        {
            bool err = false; //флаг при ошибки в данных

            if (lDeck.Count <= 1) return false;

            int start = 0;
            var tmpCard = lDeck[start];
            lDeck.RemoveAt(start);

            //Буферный маршрут
            var tmp_lDeck = new List<Card>(lDeck.Count);
            tmp_lDeck.Add(tmpCard);

            //Вставка подходящей карточки либо в начало
            //либо в конец
            for (int j = 0; j < lDeck.Count; j++)
            {
                if (tmpCard.ArrivCity == lDeck[j].DepCity)
                {
                    tmpCard = lDeck[j];
                    tmp_lDeck.Add(tmpCard);

                    lDeck.RemoveAt(j);
                    j = -1;
                }
                else if (tmp_lDeck.First().DepCity == lDeck[j].ArrivCity)
                {
                    tmp_lDeck.Insert(0, lDeck[j]);

                    lDeck.RemoveAt(j);
                    j = -1;
                }
            }

            //Возможные вопросы и ошибки изложены выше, поэтому
            //просто меняем исходные данные:
            if (lDeck.Count > 0) err = true;
            
            lDeck = tmp_lDeck;

            return err;
        }

    }
}

// В простых случаях, когда используются упорядочные данные можно использовать
// LINQ-запрос
// предварительно данные определить в массиве
// Card[] array = new Card[] { new Card { DepCity = 3, ArrivCity = 5 }, new Card { DepCity = 9, ArrivCity = 20 }, new Card { DepCity = 1, ArrivCity = 3 }, new Card { DepCity = 5, ArrivCity = 9 }, new Card { DepCity = 20, ArrivCity = 23 }, new Card { DepCity = 20, ArrivCity = 24 } };
// public Card[] FProcMy()
//        {
//            return array.OrderBy(x => x.DepCity).ToArray();
//            // можно ориентироваться на сумму полей   return arrayX.OrderBy(x => x.DepCity+ x.ArrivCity).ToArray();
//        }
// и вновь отсортированные данные загнать в новый массив
// Вызов в теле программы  var arrayPr= FProcMy();

