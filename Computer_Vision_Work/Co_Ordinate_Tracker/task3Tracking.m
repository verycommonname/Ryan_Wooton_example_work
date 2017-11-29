function [px, py] = task3Tracking(c)

a = csvread('a.csv'); % use in command window to input data
b = csvread('b.csv');
% c = [a; b] <-- write in command window to get c
x = csvread('x.csv'); % use in command window to input data
y = csvread('y.csv');
% Above one can see the variables that have to be initiates in the command
% window, with c being the observation vector.

interval = 0.1; % The time interval
sampNum = length(c); % The number of samples
motMod = [1 interval 0 0; 0 1 0 0; 0 0 1 interval; 0 0 0 1];
motNoise = [0.1 0 0 0; 0 0.2 0 0; 0 0 0.1 0; 0 0 0 0.2];
% Above, the  covariance motion model and its relevent noise, below is the
% cartesian observation model and its relevent noise.
cartMod = [1 0 0 0; 0 0 1 0];
obserNoise = [0.3 0; 0 0.3];
initState = [0 0 0 0]'; % the initial state of the model
initStateCovar = motNoise; %the covariance of the initial state
z = zeros(4,sampNum); % an empty matrix to be used to plot each sample
    for n = 1 : sampNum % a loop to go through all of the samples
        [var1, var2] = task3Predict(initState, initStateCovar, motMod, motNoise);
        [initState, initStateCovar] = task3Update(var1, var2, cartMod, obserNoise, c(:,n));
        % Above the progrma calls the predict and update classes.
        z(:,n) = initState; % The program saves the current state
    end

R = mad(c,1);
S = mad(c,0); 
% Above the program calculates the mean and median absolute deviation using
% the mad function, the first calculating the mean value and the second
% calcualting the median value.

px = z(1,:);
py = z(3,:);
% Here the program sets the co-ordinates of x and y.

Actual = [x;y];
Predicted = [px;py];
% Here the program creates two arrays, one for the x and y co-ordinates and
% one for the predicted x and y co-ordinates.

% First calculate the "error".
err = Actual - Predicted;
% Then "square" the "error".
squareError = err.^2;
% Then take the "mean" of the "square-error".
meanSquareError = mean(squareError);
% Then take the "root" of the "mean-square-error" to get 
% the root-mean-square-error! 
rootMeanSquareError = sqrt(meanSquareError);
% To calculate the root mean squared error, the program begins by deducing
% the error by sutracting the predicted outcome from the actual outcome,
% the program then squares this value before finding the mean, finally the
% program find the square root to get the final result.

subplot(2,2,1);
plot(a, b, '.r');
hold;
plot(x, y, '.g');
plot(px, py, '.b');
legend('Noisy data','Actual Data','Predicted data')
title('Kalman prediction');
% Here the program plots the noisy, predicted and actual data  into a
% chart.

fprintf('RMSE = %f\n', rootMeanSquareError);
fprintf('Mean absolute error = %f\n', S);
fprintf('Median absolute error = %f\n', R);
subplot(2,2,2);
plot(rootMeanSquareError,'^r');
title('Root mean square error');
subplot(2,2,3);
plot(S,'+r');
title('Median absolute error');
subplot(2,2,4);
plot(R,'*r');
title('Mean absolute error');
% Here the program charts the means absolute deviation, the median absolute
% deviation and the root mean squared error.
end