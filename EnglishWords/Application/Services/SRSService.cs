// namespace EnglishWords.Application.Services;
//
// public class SRSService
// {
//     public void UpdateWordReview(EnglishWord word, int quality) // Calidad: 0-5
//     {
//         if (quality < 3)
//         {
//             word.Repetitions = 0;
//             word.Interval = 1;
//         }
//         else
//         {
//             word.EaseFactor = Math.Max(1.3, word.EaseFactor + (0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02)));
//             if (word.Repetitions == 0) word.Interval = 1;
//             else if (word.Repetitions == 1) word.Interval = 6;
//             else word.Interval = (int)Math.Round(word.Interval * word.EaseFactor);
//             word.Repetitions++;
//         }
//         word.NextReviewDate = DateTime.UtcNow.AddDays(word.Interval);
//     }
// }