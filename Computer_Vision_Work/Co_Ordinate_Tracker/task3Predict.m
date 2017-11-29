function [var1, var2] = task3Predict(initState, initStateCovar, motMod, motNoise)
% Above the class reads in the state vector, the covariance matrix, the
% motion model and the motion noise in order to predict the state vector
% and the covariance of the Kalman filter.

var1 = motMod * initState;
% To predict the state of the current step of the Kalman filter, the 
% program multiplies the matrix of the motion model and the state vector.
var2 = motMod * initStateCovar * motMod' + motNoise;
% To predict the state covariance of the current step of the Kalman filter,
% the program multiplies the cureent and previous matrix of the motion
% model by the covariance matrix, this value then has the motion noise
% matrix added to it.

end
