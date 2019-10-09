using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomMath
{
    public static int WeightedRandomize(int[] weights){
        int result = 0;
        int totalWeight = 0;
        foreach(int i in weights){
            totalWeight += i;//adding up all values in the weights to see how heavy the whole array is.
        }
        int rawRandom = Random.Range(0, totalWeight);
        int counter = 0;
        for (int i = 0; i < weights.Length; i++){
            if(rawRandom<counter+weights[i]){//count up throughout each weight block and if the random number is inside one of the weight blocks return that index
                return i;
            }
                counter += weights[i];
        }
        return result;
    }

    public static float WeightedRandomize(float[] weights)
    {
        float result = 0;
        float totalWeight = 0;
        foreach (int i in weights)
        {
            totalWeight += i;//adding up all values in the weights to see how heavy the whole array is.
        }
        float rawRandom = Random.Range(0, totalWeight);
        float counter = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (rawRandom < counter + weights[i])
            {//count up throughout each weight block and if the random number is inside one of the weight blocks return that index
                return i;
            }

                counter += weights[i];

        }
        return result;
    }
}
