using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public class SecuritySystem
    {
        public List<ColorChip> FindLongestValidSequence(List<ColorChip> chips)
        {
            List<ColorChip> bestSequence = new List<ColorChip>();
            List<ColorChip> currentSequence = new List<ColorChip>();
            bool[] used = new bool[chips.Count];

            DFS(Color.Blue, chips, used, currentSequence, ref bestSequence);

            return bestSequence.Count > 0 ? bestSequence : null;
        }

        private void DFS(Color currentColor, List<ColorChip> chips, bool[] used, List<ColorChip> currentSequence, ref List<ColorChip> bestSequence)
        {
            // Check if current sequence ends in Green and is better
            if (currentColor == Color.Green)
            {
                if (currentSequence.Count > bestSequence.Count)
                {
                    bestSequence = new List<ColorChip>(currentSequence);
                }
                return;
            }

            for (int i = 0; i < chips.Count; i++)
            {
                if (!used[i])
                {
                    var chip = chips[i];

                    if (chip.StartColor == currentColor)
                    {
                        used[i] = true;
                        currentSequence.Add(chip);
                        DFS(chip.EndColor, chips, used, currentSequence, ref bestSequence);
                        currentSequence.RemoveAt(currentSequence.Count - 1);
                        used[i] = false;
                    }
                    else if (chip.EndColor == currentColor)
                    {
                        // Flip the chip
                        var flippedChip = new ColorChip(chip.EndColor, chip.StartColor);
                        used[i] = true;
                        currentSequence.Add(flippedChip);
                        DFS(flippedChip.EndColor, chips, used, currentSequence, ref bestSequence);
                        currentSequence.RemoveAt(currentSequence.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }

        public string FormatSequence(List<ColorChip> sequence)
        {
            List<string> formatted = new List<string> { "Blue" };

            foreach (var chip in sequence)
            {
                formatted.Add($"[{chip.StartColor}, {chip.EndColor}]");
            }

            formatted.Add("Green");

            return string.Join(" -> ", formatted);
        }

        public bool ValidateSequence(List<ColorChip> sequence)
        {
            if (sequence == null || sequence.Count == 0)
                return false;

            Color expectedColor = Color.Blue;

            foreach (var chip in sequence)
            {
                if (chip.StartColor != expectedColor)
                    return false;

                expectedColor = chip.EndColor;
            }

            return expectedColor == Color.Green;
        }
    }
}
