import UnityEngine
import tensorflow.lite as tflite
from pandas import read_csv
import numpy as np
import math

def convert_df_to_numpy(df):
  posX = df['posX'].to_numpy()
  posY = df[' posY'].to_numpy()
  posZ = df[' posZ'].to_numpy()
  angVelX = df[' angVelX'].to_numpy()
  angVelY = df[' angVelY'].to_numpy()
  angVelZ = df[' angVelZ'].to_numpy()
  accX = df[' accX'].to_numpy()
  accY = df[' accY'].to_numpy()
  accZ = df[' accZ'].to_numpy()
  rotX = df[' rotX'].to_numpy()
  rotY = df[' rotY'].to_numpy()
  rotZ = df[' rotZ'].to_numpy()
  rotW = df[' rotW'].to_numpy()
  angAccX = df[' angAccX'].to_numpy()
  angAccY = df[' angAccY'].to_numpy()
  angAccZ = df[' angAccZ'].to_numpy()
  loaded = [posX, posY, posZ, angVelX, angVelY, angVelZ, accX, accY, accZ, rotX, rotY, rotZ, rotW, angAccX, angAccY, angAccZ]
  return np.dstack(loaded)

def get_s(j,L,L_prime):
  term = j*L/L_prime
  if term < 1:
    return 1
  elif 1 <= term <= L:
    return math.floor(term)
  elif term > L:
    return L
  else:
    return 0 

def unify_sample_length(take):
  #input numpy array with shape   (1, L ,16)
  #returns numpy array with shape (1, L',16)
  output = np.zeros((1,200,16), dtype=np.float32) #preprocessed results of feature 1 and so on,
  L = np.shape(take)[1] #this is L
  L_prime = 200 #this is L', the value is predetermined from the start
  for j in range(0, 200): # do it 200 times
    for i in range(0, 16): #since we have 16 features
      #calculate the new value of the feature
      term_1 = get_s(j,L,L_prime)
      term_2 = j*L/L_prime*float(take[0][term_1][i])
      term_3 = j*L/L_prime
      term_4 = get_s(j,L,L_prime)
      term_5 = take[0][term_1][i]
      recalculated_feature = term_1 + 1 - term_2 + (term_3 - term_4)*term_5
      output[0][j][i] = recalculated_feature
  return output

interpreter = tflite.Interpreter(UnityEngine.Application.streamingAssetsPath + "/model.tflite")
interpreter.allocate_tensors()

# Get input and output tensors.

input_details = interpreter.get_input_details()
output_details = interpreter.get_output_details()
data_csv = read_csv(UnityEngine.Application.dataPath + "/SwingData/correct/Forehand (1).csv")
data = convert_df_to_numpy(data_csv)
sample = unify_sample_length(data)
# Test the model on random input data.
input_shape = input_details[0]['shape']
print(input_shape)
input_data = sample
interpreter.set_tensor(input_details[0]['index'], input_data)

interpreter.invoke()
output_data = interpreter.get_tensor(output_details[0]['index'])
classes = ['correct','hammergrip','elbow_strain','no_elbow_swing']


print(classes[np.argmax(output_data[0])])