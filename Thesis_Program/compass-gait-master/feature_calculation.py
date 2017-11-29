from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase

# Created by Ryan Wooton - WOO13394001
#
# Function - This program calculates the psi matrix

def featureCalc(zValue, c, h):

    psiMatrix = np.zeros((len(zValue), len(c))) # Here the program initialises the psi matrix array.

    for i in range(len(c)):

        psiVector = np.exp(-0.5 * ((zValue - c[i])**2)/ h)
        psiMatrix[:, i] = psiVector
        # Above the program calculates each individual psi vector value.

    for i in range(len(zValue)):
        psiMatrix[i, :] = psiMatrix[i, :] / sum(psiMatrix[i, :])

    for i in range(len(c)):

        psiMatrix[:,i] = psiMatrix[:,i] * zValue

    # Above the program normalises the psi matrix.

    return psiMatrix # The function returns the normalised psi matrix array.

if __name__ == "__main__":
    c = np.linspace(0,1,10) # The initialisation of the features.
    h = 0.01 # The variance value.

    zValue = computePhase() # Getting the phase values from the previous stage.
    psiNorm = featureCalc(zValue, c, h) # Calls the feature calculation function to get the psi matrix.

    plt.plot(psiNorm)
    plt.ylabel('psi value')
    plt.xlabel('t value')
    plt.show()
    # Above charts the psi matrix.