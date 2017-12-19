using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
//using System.Windows.Input;

namespace BitcoinAnalyser
{

	public partial class GraphForm : Form
	{
		const string PATH = "c:/bitcoins/";

		enum ScrollType {ScrollX, ScrollY, ZoomX, ZoomY}

		public static GraphForm that;

		
		bool draw12, draw3, drawMax, drawMin, drawCur, drawBS, drawPeaks;
		bool[] drawSB = new bool[2];
		bool[] drawData = new bool[5];

		bool blockRefresh = false;

		ScrollType _scrollType = ScrollType.ScrollY;

		PanelInfo info = new PanelInfo();

		public List<TradeMarker> _markerList = new List<TradeMarker>();

		Point oldMousePos; // drag and drop

		public bool RecordOfferData { get { return cb_record.Checked; } }

		public bool SnapToNow
		{
			get { return cb_snapToNow.Checked; }
			set { blockRefresh = true; cb_snapToNow.Checked = value; blockRefresh = false; }
		}

		//---------------------- draw functions

		public float GetValue(int y)
		{
			return info.ValLimitLowest + (info.ValLimitHighest-info.ValLimitLowest)*((float)drawPanel.Size.Height - y +info.offset.Y) / drawPanel.Height;
		}
		public DateTime GetDate(int x)
		{
			return new DateTime(info.TimeLimitEarliest.Ticks +(long)((info.TimeLimitLatest - info.TimeLimitEarliest).Ticks * ((float)x / drawPanel.Width)));
		}

		public static int RePosY(float y, float height, float val, float min, float max)
		{
			double q = (val - min) / (max - min);

			return (int)(y + height - height * q);
		}
		public static int RePosX(int x, float width, DateTime date, DateTime earliest, DateTime latest)
		{
			return (int)(x + width * (date - earliest).TotalMinutes / (latest - earliest).TotalMinutes);
		}

		public int RePosY(float val)
		{
			return RePosY(info.offset.Y, drawPanel.Height, val, info.ValLimitLowest, info.ValLimitHighest);
		}
		public int RePosX(DateTime date)
		{
			return RePosX((int)info.offset.X, drawPanel.Width, date, info.TimeLimitEarliest, info.TimeLimitLatest);
		}

		public void DrawMark(GraphMark mark, float val, DateTime date)
		{
			if (mark.visible)
			{
				drawPanel.CreateGraphics().DrawEllipse(mark.pen, RePosX(date) - mark.radius, RePosY(val) - mark.radius, mark.radius * 2, mark.radius * 2);
			}
		}

		void HidePeaks(List<DataSet> linked, int set, int type)
		{
			int j = 0;

			bool offUp = false;
			bool offDown = false;

			//---- hide extreme peaks
			if (cb_hidePeak.Checked)
			{
				foreach (var e in linked)
				{
					if (j > 1)
					{
						var l0 = linked[j - 2].setData[set].data[type];
						var l1 = linked[j - 1].setData[set].data[type];
						var l2 = linked[j].setData[set].data[type];

						if (offDown)
						{
							linked[j - 1].setData[set].data[type] = (l0 + l2) / 2;

							offDown = false;
						}
						if (offUp)
						{
							linked[j - 1].setData[set].data[type] = (l0 + l2) / 2;

							offUp = false;
						}

						//----- find peak
						float rel = linked[j].setData[set].data[type] / linked[j - 1].setData[set].data[type];

						//float rel = pB[j].Y / (float)pB[j-1].Y ;
						if (rel < 0.995f)
						{
							offDown = true;
						}
						if (rel > 1.005f)
						{
							offUp = true;
						}
					}
					j++;
				}
			}
		}

		void DrawTradeGraph(int type, Color color, int thickness)
		{
			List<TradeData> list;

			if (JsonReader.that.GetData(info.TimeLimitEarliest, info.TimeLimitLatest, out list))
			{
				Point[] pB = new Point[list.Count];

				for (int i = 0; i < list.Count; i++)//list.Count-1; i >= 0; i--)
				{
					pB[i].X = RePosX(list[list.Count -1 -i].date);
					pB[i].Y = RePosY(list[list.Count -1 -i].data[type]);
				}

				Pen pen = new Pen(color, thickness);
				if (pB.Length >= 2)
					drawPanel.CreateGraphics().DrawLines(pen, pB);
			}
		}

		void DrawOfferGraph(int type, int buy, Color color, int thickness)
		{
			List<DataSet> list;

			DateTime time = DateTime.Now;

			float iX = 0;

			List<DataSet> linked = new List<DataSet>();

			Pen pen = new Pen(color, thickness);

			if (OfferRecorder.that.GetData(info.TimeLimitEarliest, info.TimeLimitLatest, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].time < time.AddMinutes(-1.5) || i == 0 || i == list.Count - 1)
					{
						if (linked.Count > 1)
						{
							int j = 0;

							bool offUp = false;
							bool offDown = false;

							//---- hide extreme peaks
							if (cb_hidePeak.Checked)
							{
								foreach (var e in linked)
								{
									if (j > 1)
									{
										var l0 = linked[j - 2].setData[buy].data[type];
										var l1 = linked[j - 1].setData[buy].data[type];
										var l2 = linked[j].setData[buy].data[type];

										if (offDown)
										{
											/*if (l1 < l2)
											{
												linked[j - 1].setData[buy].data[type] = (l0 + l2) / 2;
											}
											else
											{
												if (j == 2)
												{
													linked[0] = linked[1];
												}
											}*/
											linked[j - 1].setData[buy].data[type] = (l0 + l2) / 2;

											offDown = false;
										}
										if (offUp)
										{
											/*if (pB[j - 1].Y > pB[j].Y)
											{
												pB[j - 1].Y = (pB[j - 2].Y + pB[j].Y) / 2;
											}*/
											linked[j - 1].setData[buy].data[type] = (l0 + l2) / 2;

											offUp = false;
										}

										//----- find peak
										float rel = linked[j].setData[buy].data[type] / linked[j - 1].setData[buy].data[type];

										//float rel = pB[j].Y / (float)pB[j-1].Y ;
										if (rel < 0.995f)
										{
											offDown = true;
										}
										if (rel > 1.005f)
										{
											offUp = true;
										}
									}
									j++;
								}
							}

							//---- smooth graph
							if (cb_smooth.Checked && type == (int)DataPointType.AVERAGE)
							{
								int n = (int)num_smooth.Value;

								List<DataSet> newList = new List<DataSet>();
								for (j = 0; j < linked.Count; j++)
								{
									if (linked.Count - j < n)
									{
										newList.Add(linked[j]);
									}
									else
									{
										DataSet av = new DataSet();

										long timeCount = 0;

										for (int j2 = 0; j2 < n; j2++)
										{
											double j2_1 = j2 + 1;
											double m = j2 / (j2_1);

											timeCount = (long)((timeCount * m) + linked[j + j2].time.Ticks / j2_1);
											for (int j3 = 0; j3 < 2; j3++)
											{
												av.setData[j3].data[(int)DataPointType.AVERAGE] += linked[j + j2].setData[j3].data[(int)DataPointType.AVERAGE];
											}
										}

										av.time = new DateTime(timeCount);

										for (int j3 = 0; j3 < 2; j3++)
										{
											av.setData[j3].data[(int)DataPointType.AVERAGE] /= n;
										}
										newList.Add(av);
									}
								}
								linked = newList;
							}


							//----

							j = 0;

							Point[] pB = new Point[linked.Count];

							foreach (var e in linked)
							{
								//iX = (float)(DateTime.Now - e.time).TotalMinutes;

								//pB[j].X = width - (int)(x + dist * (iX - 1));
								//pB[j].Y = (int)RePos(y, height, e.setData[buy].data[type], min, max); // y + height - (int)(((e.setData[buy].data[type] - min) / (max - min)) * height);

								pB[j].X = RePosX(e.time);
								pB[j].Y = RePosY(e.setData[buy].data[type]);

								j++;
							}

							linked.Clear();

							drawPanel.CreateGraphics().DrawLines(pen, pB);

							time = list[i].time;
						}

						iX += (float)(time - list[i].time).TotalMinutes;


					}

					linked.Add(list[i]);

					time = list[i].time;
				}
			}
		}

		void DrawPhaseLine()
		{

		}

		void DrawProcessedDataGraph(Color color, float thickness, int type)
		{
			const float RELATIVE_MAX = 0.1f;

			float yPos = drawPanel.Height *(1- (float)tb_procPos.Value / tb_procPos.Maximum);
			List<DataSet> list;

			DateTime time = DateTime.Now;

			float iX = 100;

			List<DataSet> linked = new List<DataSet>();

			Pen pen = new Pen(color, thickness);

			drawPanel.CreateGraphics().DrawLine(new Pen(Color.Black), 0, yPos, drawPanel.Width, yPos);

			if (OfferRecorder.that.GetData(info.TimeLimitEarliest, info.TimeLimitLatest, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					bool positive = false;
					bool signChange = false;

					if(i > 0 && type == (int)DataPointProcType.DIF)
					{
						positive = list[i-1].setData[2].data[(int)DataPointProcType.DIF] -1 > 0;
						signChange = (list[i].setData[2].data[(int)DataPointProcType.DIF]-1) * (list[i - 1].setData[2].data[(int)DataPointProcType.DIF] -1) < 0;
					}

					if (list[i].time < time.AddMinutes(-1.5) || i == 0 || signChange || i == list.Count - 1)
					{
						if (type == (int)DataPointProcType.DIF)
						{
							if (positive)
							{
								pen = Pens.Green;
							}
							else
							{
								pen = Pens.Red;
							}
						}

						if (linked.Count > 1)
						{

							//----

							HidePeaks(linked, 2, type);

							int j = 0;

							Point[] pB = new Point[linked.Count];

							bool score = type >= (int)DataPointProcType.TREND_SCORE;
							float ampf = GetAmplifier((DataPointProcType)type);

							foreach (var e in linked)
							{
								pB[j].X = RePosX(e.time);


								float val = score ? (float)(Math.Atan(e.setData[2].data[type]) / (Math.PI / 2.0f)) : (e.setData[2].data[type] - 1);

								if(score)
								{
									val = val * RELATIVE_MAX / 3;
								}

								pB[j].Y = RePosY(yPos - drawPanel.Height / 2, drawPanel.Height, 1 + val * ampf, 1f - RELATIVE_MAX, 1f + RELATIVE_MAX);

								j++;
							}

							linked.Clear();

							drawPanel.CreateGraphics().DrawLines(pen, pB);

							time = list[i].time;
						}

						iX += (float)(time - list[i].time).TotalMinutes;


					}

					linked.Add(list[i]);

					time = list[i].time;
				}
			}
		}

		void DrawString(string txt, int x, int y)
		{
			System.Drawing.Graphics formGraphics = this.CreateGraphics();
			System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
			System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
			System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
			drawPanel.CreateGraphics().DrawString(txt, drawFont, drawBrush, x, y, drawFormat);
		}

		void DrawLineH(float val, bool topText, Pen pen, bool showText = true)
		{
			string output = val.ToString("N2") + " €";

			val = RePosY(val);

			DrawString(output, 0, (int)val - (topText ? 25 : (-5)));

			drawPanel.CreateGraphics().DrawLine(pen, 0, val, drawPanel.Width, val);
		}
		void DrawLineV(DateTime date, bool showText, Pen pen)
		{
			string output = date.ToString();

			int posX = RePosX(date);

			DrawString(output, posX - 50, drawPanel.Height -20);

			drawPanel.CreateGraphics().DrawLine(pen, posX, 0, posX, drawPanel.Height);
		}

		public void DrawLine(GraphLine line, DateTime date, float val)
		{
			if (line.visible)
			{
				if (line.horizontal)
				{
					DrawLineH(line.evaluationDel == null ? val : line.evaluationDel(val), false, line.pen);
				}
				else
				{
					DrawLineV(date, true, line.pen);
				}
			}
		}

		//---------------------- controll functions

		public void AddMarker(DateTime date, TradeAnalysation data, bool custom)
		{
			TradeMarker newMarker = new TradeMarker();

			newMarker.Init(date, data, custom);

			RegisterMarker(newMarker);

			RefreshGraph();
			SaveMarker();
		}

		public void RegisterMarker(TradeMarker mark)
		{
			string t = mark.date.ToString();
			ListViewItem item = new ListViewItem(t, lv_marker.Groups[mark.custom ? 0 : 1]);

			lv_marker.Items.Add(item);

			_markerList.Add(mark);
		}

		public void ChangeViewPoint(float dx, float dy)
		{
			info.offset.Y += (dy * drawPanel.Height);

			info.TimeLimitLatest = info.TimeLimitLatest.AddMinutes(-dx*info.timeSpan.TotalMinutes );
		}

		public void CenterToTime(DateTime date)
		{
			info.TimeLimitLatest = date.AddTicks(info.timeSpan.Ticks / 2);
			RefreshGraph();
		}

		//---------------------- refresh functions

		public void RefreshGraph()
		{
			//Point pos = new Point(0, yOff), size = new Point(drawPanel.Size.Width, drawPanel.Size.Height);
			
			//----- prepare drawing
			RefreshDrawSettings();
			
			drawPanel.CreateGraphics().Clear(drawPanel.BackColor);

			//----- determine min/max
			float totalMin = -1, totalMax = -1;
			float min3, max3;

			JsonReader.that.GetMinMax(ref totalMin, ref totalMax, info.TimeLimitEarliest, info.TimeLimitLatest, (int)TradeDataType.AVG_3H);
			min3 = totalMin;
			max3 = totalMax;
			JsonReader.that.GetMinMax(ref totalMin, ref totalMax, info.TimeLimitEarliest, info.TimeLimitLatest, (int)TradeDataType.AVG_12H);

			OfferRecorder.that.GetMinMax(ref totalMin, ref totalMax, info.TimeLimitEarliest, info.TimeLimitLatest, 0, (int)DataPointType.AVERAGE);
			OfferRecorder.that.GetMinMax(ref totalMin, ref totalMax, info.TimeLimitEarliest, info.TimeLimitLatest, 1, (int)DataPointType.AVERAGE);


			info.ValLimitLowest = totalMin;
			info.ValLimitHighest = totalMax;
			
			//----- draw 3h graph
			if (draw3)
			{
				DrawTradeGraph((int)TradeDataType.AVG_3H, System.Drawing.Color.Blue, 1);
			}
			//----- draw 12h graph
			if (draw12)
			{
				DrawTradeGraph((int)TradeDataType.AVG_12H, System.Drawing.Color.Red, 1);
			}

			//------ draw horizontal value lines
			if (drawMin)
				DrawLineH(min3, false, new Pen(Color.Black));
			if (drawMax)
				DrawLineH(max3, true, new Pen(Color.Black));
			if (drawCur)
				DrawLineH(JsonReader.that.GetCurrentValue((int)TradeDataType.AVG_3H), true, new Pen(Color.Black));


			//------ draw current buy/sell average
			if (drawBS)
			{
				Pen pink = new Pen(System.Drawing.Color.Pink, 1);

				Pen green = new Pen(System.Drawing.Color.Green, 1);

				float v = OfferBook.that.AvgFee(true, 5);

				float yV = RePosY(OfferBook.that.AvgFee(true, 10));
				drawPanel.CreateGraphics().DrawEllipse(pink , drawPanel.Width, yV, 5, 5);

				yV = RePosY(OfferBook.that.AvgFee(false, 10));
				drawPanel.CreateGraphics().DrawEllipse(green, drawPanel.Width, yV, 5, 5);
			}

			//------ draw offer graphs
			for (int b = 0; b < 2; b++)
			{
				for (int i = 0; i < 3; i++)
				{
					if (drawSB[b] && drawData[i])
					{
						DrawOfferGraph(i, b, b == 0 ? Color.Pink : Color.Green, i);
					}
				}
			}

			//----- draw dif graph
			if(cb_buyDivSell.Checked)
				DrawProcessedDataGraph(Color.Violet, 1, (int)DataPointProcType.DIF);
			if (cb_gradBuy.Checked)
				DrawProcessedDataGraph(Color.Pink, 1, (int)DataPointProcType.GRAD_BUY);
			if (cb_gradSell.Checked)		
				DrawProcessedDataGraph(Color.Green, 1, (int)DataPointProcType.GRAD_SELL);
			if (cb_gradBuyDivGradSell.Checked)
				DrawProcessedDataGraph(Color.Gray, 1, (int)DataPointProcType.REL_CHANGE);
			if (cb_score.Checked)
				DrawProcessedDataGraph(Color.Orange, 1, (int)DataPointProcType.TREND_SCORE);
			if (cb_buyScore.Checked)
				DrawProcessedDataGraph(Color.Pink, 1, (int)DataPointProcType.BUY_SCORE);
			if (cb_sellScore.Checked)
				DrawProcessedDataGraph(Color.Green, 1, (int)DataPointProcType.SELL_SCORE);
			if (cb_mixedScore.Checked)
				DrawProcessedDataGraph(Color.Blue, 1, (int)DataPointProcType.MIXED_SCORE);

			foreach(var mark in _markerList)
			{
				mark.Draw();
			}


			//draw cursor marker
			if (CursorH.Checked)
			{
				DrawLineH(GetValue(oldMousePos.Y), false, new Pen(Color.Black));
			}
			if (CursorV.Checked)
			{
				DrawLineV(GetDate(oldMousePos.X), true, new Pen(Color.Black));
			}
		}

		public void RefreshDrawSettings()
		{
			draw12 = cb_12.Checked;
			draw3 = cb_3.Checked;
			drawSB[0] = cb_buy.Checked;
			drawSB[1] = cb_sell.Checked;
			drawData[(int)DataPointType.AVERAGE] = cb_av.Checked;
			drawData[(int)DataPointType.PEAK] = cb_peak.Checked;
			drawData[(int)DataPointType.PEAK_SMALL] = cb_peakSmall.Checked;
			drawPeaks = cb_peaks.Checked;
			drawBS = cb_bs.Checked;
			drawMin = cb_min.Checked;
			drawMax = cb_max.Checked;
			drawCur = cb_cur.Checked;

			if(cb_snapToNow.Checked)
			{
				info.TimeLimitLatest = DateTime.Now;
			}

			info.padding = 150;
			int.TryParse(tb_padding.Text, out info.padding);

			float hours = 24;
			float.TryParse(GraphTimeBoxH.Text.Replace('.', ','), out hours);
			info.timeSpan = new TimeSpan(0, 0, (int)(hours * 3600.0));
			

		}

		int GetSelectedMarker()
		{
			if (lv_marker.SelectedIndices.Count != 0)
			{
				return lv_marker.SelectedIndices[0];
			}
			return -1;
		}

		void RefreshMarker()
		{
			int id = GetSelectedMarker();

			if(id != -1)
			{
				_markerList[id].lines[0].visible = cb_dateLine.Checked;
				_markerList[id].lines[1].visible = cb_priceLine.Checked;
				_markerList[id].lines[2].visible = cb_0perc.Checked;
				_markerList[id].lines[3].visible = cb_1perc.Checked;

				_markerList[id].visible = cb_markerVisibility.Checked;

				RefreshGraph();
			}
		}

		//----------------------- Save/Load

		public void SaveMarker()
		{
			XmlDocument doc = new XmlDocument();
			
			XmlElement root = doc.CreateElement("marker_log");

			doc.AppendChild(root);

			foreach (var mark in _markerList)
			{
				mark.Save(root, doc);
			}

			System.IO.Directory.CreateDirectory(PATH);

			string path = PATH + "marker.xml";

			doc.Save(path);
		}

		public void LoadMarker()
		{
			XmlDocument doc = new XmlDocument();

			string url = PATH + "marker.xml";

			if (System.IO.File.Exists(url))
			{
				doc.Load(PATH + "marker.xml");
				XmlElement root = doc.DocumentElement;

				foreach(XmlElement el in root.GetElementsByTagName("trade_marker"))
				{
					TradeMarker newMarker = new TradeMarker();
					newMarker.Load(el);
					RegisterMarker(newMarker);
				}
			}

		}

		//----------------------- init
		
		public GraphForm()
		{
			that = this;

			InitializeComponent();

			this.MouseWheel += panel2_MouseWheel;

			Show();
		}

		private void GraphForm_Load(object sender, EventArgs e)
		{
			info.TimeLimitLatest = DateTime.Now;

			LoadMarker();

			OfferRecorder.that.Load(DateTime.Now.AddDays(-7));

		}

		//----------------------- events

		private void RefreshCourseGraph_Tick(object sender, EventArgs e)
		{
			JsonReader.that.Reload3MonthJson();
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			RefreshGraph();
		}

		//show info in status menu
		private void drawPanel_MouseMove(object sender, MouseEventArgs e)
		{
			label_time.Text = GetDate(e.Location.X).ToString();
			label_value.Text = GetValue(e.Location.Y).ToString("N2") + " €";
			
			Point pos = e.Location;

			bool refresh = false;

			refresh = CursorH.Checked ||CursorV.Checked;

			if(System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))	
//			if (System.Windows.Input.Mouse.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{

				Point dif = Point.Subtract(pos, (Size)oldMousePos);
				ChangeViewPoint(dif.X / (float)drawPanel.Width, dif.Y / (float)drawPanel.Height);

				refresh = true;

			}
			oldMousePos = pos;

			if(refresh)
			{
				RefreshGraph();
			}
		}

		//focus control for scrolling
		private void drawPanel_MouseHover(object sender, EventArgs e)
		{
			drawPanel.Focus();
		}

		//scroll into graph
		private void panel2_MouseWheel(object sender, MouseEventArgs e)
		{
			RefreshDrawSettings();

			int change = e.Delta / 120;

			switch(_scrollType)
			{
				case ScrollType.ScrollX:
					ChangeViewPoint(5 * change / 100, 0);
					break;
				case ScrollType.ScrollY:
					ChangeViewPoint(0, -5 * change / 100);
					
					break;
				case ScrollType.ZoomX:
					info.timeSpan = new TimeSpan((long)(info.timeSpan.Ticks * (1.0f - 0.01* change)));
					GraphTimeBoxH.Text = info.timeSpan.TotalHours.ToString();
					break;
				case ScrollType.ZoomY:
					info.padding += (int)(-20 * change);
					tb_padding.Text = info.padding.ToString();
					break;
			}


			RefreshGraph();
		}

		//change draw setting
		private void drawSettingCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if(!blockRefresh)
				RefreshGraph();
		}

		//enable/disable course data refreshment
		private void cbRefreshJson_CheckedChanged(object sender, EventArgs e)
		{
			RefreshCourseGraph.Enabled = cb_refreshJson.Checked;
		}

		//refresh json data
		private void button_refreshCourse_Click(object sender, EventArgs e)
		{
			JsonReader.that.Reload3MonthJson();
			RefreshGraph();
		}

		//repaint graph
		private void button4_Click(object sender, EventArgs e)
		{
			RefreshGraph();
		}

		//reload offer records
		private void button5_Click(object sender, EventArgs e)
		{
			OfferRecorder.that.Load(DateTime.Now.AddDays(-7));
			RefreshGraph();
		}

		//change timespan
		private void GraphTimeBoxH_TextChanged(object sender, EventArgs e)
		{
			RefreshGraph();
		}

		private void cb_autoReload_CheckedChanged(object sender, EventArgs e)
		{
			if (cb_autoReload.Checked)
			{
				cb_record.Checked = false;
			}
		}

		private void cb_record_CheckedChanged(object sender, EventArgs e)
		{
			if (cb_record.Checked)
			{
				cb_autoReload.Checked = false;
			}
			else
			{
				cb_autoReload.Checked = true;
			}
		}

		private void AutoReloadOfferData_Tick(object sender, EventArgs e)
		{
			if(cb_autoReload.Checked)
			{
				OfferRecorder.that.Load(DateTime.Now.AddDays(-7));

				RefreshGraph();
			}
		}

		void UncheckTools()
		{
			ZoomY.Checked = false;
			ZoomX.Checked = false;
			ScrollX.Checked = false;
			ScrollY.Checked = false;
		}

		private void ZoomY_Click(object sender, EventArgs e)
		{
			_scrollType = ScrollType.ZoomY;
			UncheckTools();
			ZoomY.Checked = true;
		}

		private void ScollX_Click(object sender, EventArgs e)
		{
			_scrollType = ScrollType.ScrollX;
			UncheckTools();
			ScrollX.Checked = true;
		}

		private void ZoomX_Click(object sender, EventArgs e)
		{
			_scrollType = ScrollType.ZoomX;
			UncheckTools();
			ZoomX.Checked = true;
		}

		private void ScrollY_Click(object sender, EventArgs e)
		{
			_scrollType = ScrollType.ScrollY;
			UncheckTools();
			ScrollY.Checked = true;
		}

		private void MarkTool_Click(object sender, EventArgs e)
		{
		}

		private void CursorTool_Click(object sender, EventArgs e)
		{
			CursorH.Checked = false;
			CursorV.Checked = false;
		}
		private void drawPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (MarkTool.Checked)
			{
				TradeAnalysation ana = new TradeAnalysation(GetValue(e.Location.Y), 0, 0.8f);
				AddMarker(GetDate(e.Location.X), ana, true);
				MarkTool.Checked = false;
			}
		}

		private void cb_dateLine_CheckedChanged(object sender, EventArgs e)
		{
			if(!blockRefresh)
				RefreshMarker();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int id = GetSelectedMarker();

			if(id != -1)
			{
				if (_markerList[id].custom)
				{
					lv_marker.Items.RemoveAt(id);
					_markerList.Remove(_markerList[id]);
				}
				else
				{
					_markerList[id].visible = false;
				}

				SaveMarker();

				RefreshGraph();
			}
		}

		private void button_saveMarker_Click(object sender, EventArgs e)
		{
			SaveMarker();
		}


		float GetAmplifier(DataPointProcType type)
		{
			const float MAX_AMP = 10;

			float res = 1;

			switch(type)
			{
				case DataPointProcType.DIF: res = tb_bds.Value; break;
				case DataPointProcType.GRAD_BUY: res = tb_gb.Value; break;
				case DataPointProcType.GRAD_SELL: res = tb_gs.Value; break;
				case DataPointProcType.REL_CHANGE: res = tb_gbdgs.Value; break;
				case DataPointProcType.TREND_SCORE: res = tb_score.Value; break;
				case DataPointProcType.BUY_SCORE: res = tb_buyScore.Value; break;
				case DataPointProcType.SELL_SCORE: res = tb_sellScore.Value; break;
				case DataPointProcType.MIXED_SCORE: res = tb_mixedScore.Value; break;
			}

			res = 1 + MAX_AMP * res/ (float)tb_bds.Maximum;
			return res;
		}

		private void drawPanel_MouseDown(object sender, MouseEventArgs e)
		{
			oldMousePos = e.Location;
		}

		private void lv_marker_SelectedIndexChanged(object sender, EventArgs e)
		{
			int id = GetSelectedMarker();

			if (id != -1)
			{
				blockRefresh = true;

				cb_dateLine.Checked = _markerList[id].lines[0].visible;
				cb_priceLine.Checked = _markerList[id].lines[1].visible;
				cb_0perc.Checked = _markerList[id].lines[2].visible;
				cb_1perc.Checked = _markerList[id].lines[3].visible;

				cb_markerVisibility.Checked = _markerList[id].visible;

				blockRefresh = false;
			}
		}

		private void lv_marker_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int id = GetSelectedMarker();

			if(id != -1)
			{
				CenterToTime(_markerList[id].date);
			}
		}




		//------------------------
	}


	public class PanelInfo
	{
		float valLimitHighest, valLimitLowest;
		public float ValLimitHighest { get { return valLimitHighest + padding; } set { valLimitHighest = value; } }
		public float ValLimitLowest { get { return valLimitLowest - padding; } set { valLimitLowest = value; } }

		DateTime timeLimitLatest;
		public TimeSpan timeSpan;
		public DateTime TimeLimitLatest { 
			get { return timeLimitLatest; }
			set
			{
				if (value >= DateTime.Now)
				{
					GraphForm.that.SnapToNow = true;
					timeLimitLatest = DateTime.Now;
				}
				else
				{
					timeLimitLatest = value;

					GraphForm.that.SnapToNow = false;
				}
			} 
		}
		public DateTime TimeLimitEarliest { get { return timeLimitLatest.AddTicks(-timeSpan.Ticks); }}


		public int padding;
		public PointF offset = new PointF(0, 20);
	}
	
	public delegate float EvalDel(float val);

	public class GraphLine
	{

		public bool horizontal;
		public Pen pen;

		Color color = Color.Black;
		float thickness = 1;
		public Color Color { set { color = value; pen = new Pen(value, thickness); } }
		public float Thickness { set { thickness = value; pen = new Pen(color, value); } }

		public bool visible = true;
		public EvalDel evaluationDel;

		public void Save(XmlElement parent, XmlDocument doc)
		{
			var node = doc.CreateElement("line");
			parent.AppendChild(node);
			node.SetAttribute("hor", horizontal.ToString());
			node.SetAttribute("color", color.ToArgb().ToString());
			node.SetAttribute("thickness", thickness.ToString());
			node.SetAttribute("visible", visible.ToString());
		}
		public void Load(XmlElement el)
		{
			bool.TryParse(el.GetAttribute("hor"), out horizontal);
			
			int argb = 0;
			int.TryParse(el.GetAttribute("color"), out argb);
			color = Color.FromArgb(argb);

			float.TryParse(el.GetAttribute("thickness"), out thickness);

			pen = new Pen(color, thickness);

			bool.TryParse(el.GetAttribute("visible"), out visible);
		}
	}
	public class GraphMark
	{
		public float radius = 2.5f;
		public Pen pen = new Pen(Color.Black);
		public bool visible;

		public Color Color { set { pen = new Pen(value); } }

		public void Save(XmlElement parent, XmlDocument doc)
		{
			var node = doc.CreateElement("mark");
			parent.AppendChild(node);
			node.SetAttribute("radius", radius.ToString());
			node.SetAttribute("color", pen.Color.ToArgb().ToString());
			node.SetAttribute("visible", visible.ToString());
		}
		public void Load(XmlElement el)
		{
			float.TryParse(el.GetAttribute("radius"), out radius);

			int argb = 0;
			int.TryParse(el.GetAttribute("color"), out argb);
			pen = new Pen(Color.FromArgb(argb), 1);

			bool.TryParse(el.GetAttribute("visible"), out visible);
		}
	}

	public class TradeMarker
	{
		public DateTime date;
		public TradeAnalysation data;
		public List<GraphLine> lines = new List<GraphLine>();
		GraphMark mark = new GraphMark();

		public bool custom;
		public bool visible = true;

		public void Load(XmlElement el)
		{
			date = DateTime.Parse(el.GetAttribute("date"));

			data = TradeAnalysation.Load(el);

			bool.TryParse(el.GetAttribute("custom"), out custom);
			bool.TryParse(el.GetAttribute("visible"), out visible);

			mark.Load((XmlElement)el.GetElementsByTagName("mark")[0]);

			InitLineList();

			int i = 0;
			foreach(XmlElement e in el.GetElementsByTagName("line"))
			{
				lines[i++].Load(e);
			}
		}

		public void Save(XmlElement parent, XmlDocument doc)
		{
			XmlElement el = doc.CreateElement("trade_marker");
			parent.AppendChild(el);

			el.SetAttribute("date", date.ToString());

			data.Save(el);

			el.SetAttribute("custom", custom.ToString());
			el.SetAttribute("visible", visible.ToString());

			mark.Save(el, doc);

			foreach(var l in lines)
			{
				l.Save(el, doc);
			}
		}

		public void SetMark(Color color, float rad = 2.5f)
		{
			mark.Color = color;
			mark.radius = rad;
			mark.visible = true;
		}

		public void Init(DateTime date, TradeAnalysation data, bool custom)
		{
			this.data = data;
			this.date = date;
			this.custom = custom;

			SetMark(Color.Black);

			InitLineList();
		}
		
		void InitLineList()
		{
			AddLine(true, Color.Black);
			AddLine(false, Color.Black);
			AddLine(true, Color.Red, 1, val => { return data.GetSellPriceForPercentageGain(0); });
			AddLine(true, Color.Green, 1, val => { return data.GetSellPriceForPercentageGain(1); });
		}

		GraphLine AddLine(bool horizontal, Color color, float thickness = 1, EvalDel evalDel = null)
		{
			GraphLine newLine = new GraphLine();

			newLine.Color = color;
			newLine.Thickness = thickness;
			newLine.visible = true;
			newLine.horizontal = horizontal;
			newLine.evaluationDel = evalDel;

			lines.Add(newLine);

			return newLine;
		}

		public void Draw()
		{
			if (visible)
			{
				GraphForm.that.DrawMark(mark, data.BuyPrice, date);

				foreach (var line in lines)
				{
					GraphForm.that.DrawLine(line, date, data.BuyPrice);
				}
			}
		}
	}

}
