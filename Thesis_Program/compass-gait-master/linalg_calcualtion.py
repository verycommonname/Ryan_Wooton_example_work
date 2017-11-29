from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase
from feature_calculation import featureCalc, c, h
from run_compass_gait import *


def linAlg(psiNorm, a, F):
    aVal = (((np.transpose(psiNorm))*psiNorm)+(a*np.eye(psiNorm.shape(1)))) # add power of minus one
    bVal = (np.transpose(psiNorm)) * F
    linAlg = np.linalg.solve(aVal,bVal)
    return linAlg

zValue = computePhase()
psiNorm = featureCalc(zValue, c, h)
w = linAlg(psiNorm, alpha, F)

plt.plot(w)
plt.ylabel('I value')
plt.xlabel('t value')
plt.show()