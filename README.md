# VisualizeOptimization

This is software which can visualize an optimization process.

Benchmaerk funticon is 2D Rosenbrock.

もともとLibOptimizationのサンプルプログラムに同様のものを入れていたのをOxyPlotというグラフライブラリを使って書き直したものです。Nelder-Mead法、実数値GA(シンプレクス法、REX法）の最適化過程を見ることができます。

![VisualizeOptimization exp](https://raw.githubusercontent.com/tomitomi3/VisualizeOptimization/master/_githubpic/explain.PNG)

Overview
========

**Init**ボタンで初期化。

**<**一つ前に戻る。

**>**一つ進む。

Sample
======

実数値GAシンプレクス法の最適化過程。ローゼンブロック関数の谷にポイントが集まっていく。

![VisualizeOptimization ga_spx](https://raw.githubusercontent.com/tomitomi3/VisualizeOptimization/master/_githubpic/ga_generation.png)

Use Library
===========

- OxyPlot (http://oxyplot.org/)
- LibOptimization (https://github.com/tomitomi3/LibOptimization)

License
=======

Microsoft Public License (MS-PL)

http://opensource.org/licenses/MS-PL

Requirements
===============

.NET Framework 4.0
