from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation

# Created by Ryan Wooton - WOO13394001
#
# Function - This program calculates the phase values

def phaseCalc(timeStamps, alpha):
    zVal = np.zeros((len(timeStamps),)) # Creates an empty array the size of the timestamps array.
    zVal[0] = 1 # Sets the first value in the zVal array.
    for i in range(len(timeStamps) - 1): # Loops up to the penultimate value.
        gradZ = - alpha * zVal[i] # Here the gradient of the z value is calculated.
        dt = timeStamps[i + 1] - timeStamps[i] # Here the time differential is calculated.
        zVal[i + 1] = zVal[i] + gradZ * dt # Here the phase is calculated.


    return zVal


def computePhase():
    numTimeStamps = 100 # The number of timesteps.
    timeStamps = np.linspace(0, 1, numTimeStamps)   # Return 100 evenly space number between 0 and 1 ot give the
                                                    # timestamps.

    alpha = 2 # The alpha value.

    zValue = phaseCalc(timeStamps, alpha) # Calling the phase calculation function.

    return (zValue, timeStamps)
    # Returns the phase values and the timesteps.