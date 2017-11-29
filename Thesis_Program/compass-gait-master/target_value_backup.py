from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase
from feature_calculation import featureCalc
from robot_vars import g
# from run_compass_gait import *
# import psi val from additional calculations


def targetCalc(zValue, psiNorm, a, b, g, y, dt):
    # psiSum = np.zeros((len(psiNorm),))
    # tSum = np.zeros((len(t),))
    zCount = np.zeros((len(zValue),))
    fCount = np.zeros((len(zValue),))
    velosity = np.zeros((len(zValue),))
    yCount = np.zeros((len(zValue),))
    trajectory = np.zeros((len(zValue),))
    tFunct = np.zeros((len(zValue),))
    linAlg = np.zeros((len(zValue),))

#    for i in zCount:
#        sumResult = sum(psiNorm[i] * w[i])
#        fCount[i] = sumResult

    velocity = np.diff(y) / dt;
    velocity = np.concatenate((np.zeros((1,)), velocity))

    acceleration = np.diff(velocity) / dt;
    acceleration = np.concatenate((acceleration, np.zeros((1,))))

    fTarget = acceleration - a * (b * (g - y) - velocity)

    return fTarget

def getParameterVec(psiNorm, fTarget, alpha): # Modify to use w in calculation except for the first version

    tempMatrix = psiNorm.transpose().dot(psiNorm) + alpha * np.eye(psiNorm.shape[1])

    w = np.linalg.solve(tempMatrix, psiNorm.transpose().dot(fTarget))

    return w

def getTrajectory(psiNorm, w, g, a, b, y0, dt): # Modify to return an array

    y = np.zeros((psiNorm.shape[0]))
    velocity = np.zeros((psiNorm.shape[0]))
    f = psiNorm.dot(w)

    y[0] = y0

    for i in range(0,psiNorm.shape[0]-1):
        acceleration = a * (b * (g - y[i]) - velocity[i]) + f[i]
        velocity[i + 1] = velocity[i] + (acceleration * dt)
        y[i + 1] = y[i] + (velocity[i + 1] * dt)

    return y


if __name__ == "__main__":
    c = np.linspace(0, 1, 30)
    h = 0.01

    zValue, time = computePhase()

    psiNorm = featureCalc(zValue, c, h)

    y = np.sin(2 * np.pi * time) * np.exp(-time * 5)
    g = y[-1]
    a = 1
    b = 1
    dt = time[1] - time[0]

    plt.figure()
    plt.plot(time, y) #

    # \/ \/ \/ create for loop \/ \/ \/

    target = targetCalc(zValue, psiNorm, a, b, g, y, dt)

    plt.figure()
    plt.plot(target)

    w = getParameterVec(psiNorm, target, 10**-6)

    fTargetPredicted = psiNorm.dot(w)
    plt.plot(fTargetPredicted, 'g')

    y0 = y[0]

    newTraj = getTrajectory(psiNorm, w, g, a, b, y0, dt)
    plt.figure()
    plt.plot(newTraj, 'r')
    plt.plot(y, 'g')
    plt.show()

    print('Hello')
