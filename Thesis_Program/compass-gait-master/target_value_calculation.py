from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase
from feature_calculation import featureCalc
from robot_vars import g

# Created by Ryan Wooton - WOO13394001
#
# Function - This program calculates the trajectory

def targetCalc(zValue, psiNorm, a, b, g, y, dt):

    velocity = np.diff(y) / dt;
    velocity = np.concatenate((np.zeros((1,)), velocity))
    # Calculates the velocity by dividing the position difference by the time differential.

    acceleration = np.diff(velocity) / dt;
    acceleration = np.concatenate((acceleration, np.zeros((1,))))
    # Calculates the acceleration by dividing the velocity difference by the time differential.

    fTarget = acceleration - a * (b * (g - y) - velocity)
    # Calculates the target location of either the longitude or the latitude.

    return fTarget

def getParameterVec(psiNorm, fTarget, alpha):

    tempMatrix = psiNorm.transpose().dot(psiNorm) + alpha * np.eye(psiNorm.shape[1])
    # Here the program calculates the first value for the 'np.linalg.solve' method by adding together the sum of the
    # psi matrix times the transposed psi matrix and the sum of alpha times the diagonally oriented version of the psi
    # matrix.

    w = np.linalg.solve(tempMatrix, psiNorm.transpose().dot(fTarget))
    # Here the program uses the 'linalg.solve' on the previously calculated value and the sum of the transposed psi
    # matrix and the target array.

    return w

def getTrajectory(psiNorm, w, g, a, b, y0, dt):

    y = np.zeros((psiNorm.shape[0]))
    velocity = np.zeros((psiNorm.shape[0]))
    # Above the program initialises two arrays the size of the psi matrix for use in the function
    f = psiNorm.dot(w) # Here the function gets the feature paratmeters by multiplying the psi matrix and the parameter
    # vector array together.

    y[0] = y0 # Initialising the first co-ordinate value.

    for i in range(0, psiNorm.shape[0] - 1):
        acceleration = a * (b * (g - y[i]) - velocity[i]) + f[i]    # Calculates the acceleration of the trajectory
                                                                    # movement.
        velocity[i + 1] = velocity[i] + (acceleration * dt)
        # Calculates the velocity of the trajectory movement.
        y[i + 1] = y[i] + (velocity[i + 1] * dt)
        # Calculates the trajectory's longitude/latitude co-ordinates.

    return y
    # Returns the co-ordinate.

if __name__ == "__main__":
    c = np.linspace(0, 1, 30) # Initialises the number of features.
    h = 0.01 # Initialises the variance.

    zValue, time = computePhase()

    psiNorm = featureCalc(zValue, c, h)

    y1 = np.sin(2 * np.pi * time) * np.exp(-time * 5)
    y2 = np.cos(2 * np.pi * time) * np.exp(-time * 5)
    # Calculates the x and y co-ordinates in separate arrays.

    y = np.vstack([y1, y2]).transpose()
    # Puts the x and y co-ordinates into a single array

    wArray = np.zeros((y.shape[1], 30)) # Initialises an empty array in the shape of the co-ordinates.

    g = y[-1, :] # The target is initialised.
    a = 1
    b = 1
    dt = time[1] - time[0]
    # Above the program initialises various values.

    plt.figure()
    plt.plot(time, y)
    # plt.figure()
    # The program plots the time values and the co-ordinates.

    for i in range(0, y.shape[1]): # a loop that gets the longitude and latitude trajectories
        target = targetCalc(zValue, psiNorm, a, b, g[i], y[:, i], dt) # Gets the target from the target function.

        plt.figure()
        plt.plot(target)
        # Plots the newly acquired target.

        w = getParameterVec(psiNorm, target, 10 ** -6)
        wArray[i, :] = getParameterVec(psiNorm, target, 10 ** -6)
        # Here the program calledd the parameter vector calculator twice, once for use within the loop and once for use
        # outside of the method.

        fTargetPredicted = psiNorm.dot(w)
        plt.plot(fTargetPredicted, 'g')
        # Here the program calculated the predicted target by multiplying the psi matrix and parameter vector array
        # together.

        y0 = y[i] # here the program set the
        # y0 = y[0]

        newTraj = getTrajectory(psiNorm, w, g[i], a, b, y[0, i], dt)
        # Here the program get the trajectory of either the longitude or the latitude (depending on the loop number).

        plt.figure()
        plt.plot(newTraj, 'r')
        plt.plot(y[:, i], 'g')
        plt.show()
        # Plots and displays the current trajectory and co-ordinates

    plt.figure()
    plt.plot(newTraj, 'r')
    plt.plot(y, 'g')
    plt.show()
    # Plots and displays the trajectory and the co-ordinates.

    print("Hello")
